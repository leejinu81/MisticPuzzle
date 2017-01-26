using Extension;
using UnityEngine;
using Zenject;

namespace Lonely
{
    public class EnemyState_Idle : EnemyState
    {
        public EnemyState_Idle(IStateEnter enter, IStateExit exit, IStateUpdate update,
                               ITurnable turnable, ITitanShield titanShield)
            : base(enter, exit, update, turnable, titanShield)
        {
        }

        public class CustomFactory : IFactory<EnemyState>
        {
            #region interface

            EnemyState IFactory<EnemyState>.Create()
            {
                return StateBinder.Bind(_container)
                                  .Enter<EnemyState_FindAndMove>()
                                  .Turn<EnemyState_FindAndMove>()
                                  .Make<EnemyState_Idle>();
            }

            #endregion interface

            private readonly DiContainer _container;

            public CustomFactory(DiContainer container)
            {
                _container = container;
            }
        }
    }

    public class EnemyState_FindAndMove : IStateEnter, ITurnable
    {
        void IStateEnter.Enter()
        {
            _model.ResetDirection();
            _model.enableTarget = false;
            FindingPlayer();
        }

        void ITurnable.Turn()
        {
            FindingPlayer();
        }

        private readonly EnemyModel _model;
        private readonly GuardianFSM _fsm;
        private readonly EnemyEye _eye;
        private readonly GameCommands.PlayerTurn _playerTurn;

        public EnemyState_FindAndMove(GuardianFSM fsm,
                               EnemyModel model, EnemyEye eye,
                               GameCommands.PlayerTurn playerTurn)
        {
            _fsm = fsm;
            _model = model;
            _eye = eye;
            _playerTurn = playerTurn;
        }

        private void FindingPlayer()
        {
            Player player;
            if (_eye.Look(out player))
            {
                MoveToPlayer(player);
            }
            else
            {
                _playerTurn.Execute();
            }
        }

        private void MoveToPlayer(Player player)
        {
            if (Vector2.Distance(_model.position, player.XY()).IsLessOrEqual(1))
            {
                player.Die();

                _model.position = player.XY();
                _fsm.ChangeState<EnemyState_Kill>();
            }
            else
            {
                _model.targetPos = player.XY();
                _fsm.ChangeState<EnemyState_MoveToTarget>();
            }
        }
    }
}