using Extension;
using UnityEngine;
using Zenject;

namespace Lonely
{
    public class PlayerState_Kill : IState, ITickable
    {
        #region Explicit Interface

        void IState.Enter()
        {
            Debug.Log("PlayerState_Kill Enter");
            _model.position = _model.movePosition;
            _enemyTurnCommand.Execute();
        }

        void IState.Exit()
        {
        }

        void ITickable.Tick()
        {
            if (IsOverMoveTime())
            {
                _fsm.ChangeState<PlayerState_Idle>();
            }
        }

        #endregion Explicit Interface

        private readonly PlayerFSM _fsm;
        private readonly PlayerModel _model;
        private readonly float _moveTime;
        private readonly GameCommands.EnemyTurn _enemyTurnCommand;

        public PlayerState_Kill(PlayerFSM fsm, PlayerModel model, float moveTime, GameCommands.EnemyTurn enemyTurnCommand)
        {
            _fsm = fsm;
            _model = model;
            _moveTime = moveTime;
            _enemyTurnCommand = enemyTurnCommand;
        }

        private bool IsOverMoveTime()
        {
            return _fsm.stateTime.IsGreater(_moveTime);
        }

        public class Factory : Factory<PlayerState_Kill>
        { }
    }
}