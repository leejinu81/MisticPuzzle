using Extension;
using UnityEngine;
using Zenject;

namespace Lonely
{
    public class EnemyState_Idle : IState, ITurnable
    {
        #region Explicit Interface

        void IState.Enter()
        {
            Debug.Log("EnemyState_Idle Enter");
            _model.ResetDirection();
            _model.enableTarget = false;
            FindingPlayer();
        }

        void IState.Exit()
        {
        }

        void ITurnable.Turn()
        {
            FindingPlayer();
        }

        #endregion Explicit Interface

        private readonly EnemyFSM _fsm;
        private readonly EnemyModel _model;
        private readonly EnemyEye _eye;
        private readonly GameCommands.PlayerTurn _playerTurn;

        public EnemyState_Idle(EnemyFSM fsm, EnemyModel model, EnemyEye eye, GameCommands.PlayerTurn playerTurn)
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

        public class Factory : Factory<EnemyState_Idle>
        { }
    }

    public interface ITurnable
    {
        void Turn();
    }
}