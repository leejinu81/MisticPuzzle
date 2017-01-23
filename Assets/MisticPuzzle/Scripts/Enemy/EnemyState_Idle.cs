using Extension;
using UnityEngine;
using Zenject;

namespace Lonely
{
    public class EnemyState_Idle : GuardianState
    {
        public EnemyState_Idle(IStateEnter enter, IStateExit exit, IStateUpdate update, ITurnable turnable)
            : base(enter, exit, update, turnable)
        {
        }

        public class CustomFactory : IFactory<GuardianState>
        {
            #region interface

            GuardianState IFactory<GuardianState>.Create()
            {
                var binder = GuardianStateBinder<EnemyState_Idle>.For(_container);
                return binder.Turn<EnemyStateIdle_Turn>().Enter<EnemyStateIdle_Enter>().Make();
            }

            #endregion interface

            private readonly DiContainer _container;

            public CustomFactory(DiContainer container)
            {
                _container = container;
            }
        }
    }

    public class EnemyStateIdle_Enter : IStateEnter
    {
        void IStateEnter.Enter()
        {
            Debug.Log("EnemyState_Idle Enter");
            _model.ResetDirection();
            _model.enableTarget = false;
            FindingPlayer();
        }

        private readonly EnemyModel _model;
        private readonly GuardianFSM _fsm;
        private readonly EnemyEye _eye;
        private readonly GameCommands.PlayerTurn _playerTurn;

        public EnemyStateIdle_Enter(GuardianFSM fsm,
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

    public class EnemyStateIdle_Turn : ITurnable
    {
        void ITurnable.Turn()
        {
            FindingPlayer();
        }

        private readonly EnemyModel _model;
        private readonly GuardianFSM _fsm;
        private readonly EnemyEye _eye;
        private readonly GameCommands.PlayerTurn _playerTurn;

        public EnemyStateIdle_Turn(EnemyModel model, GuardianFSM fsm, EnemyEye eye, GameCommands.PlayerTurn playerTurn)
        {
            _model = model;
            _fsm = fsm;
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

    public interface ITurnable
    {
        void Turn();
    }

    public abstract class Turnable : ITurnable
    {
        void ITurnable.Turn()
        {
        }

        public static readonly ITurnable Null = new NullTurnable();

        private class NullTurnable : ITurnable
        {
            void ITurnable.Turn() { }
        }
    }
}