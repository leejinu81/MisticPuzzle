using UnityEngine;
using Zenject;

namespace Lonely
{
    public class PlayerState_Kill : IState
    {
        #region Explicit Interface

        void IState.Enter()
        {
            Debug.Log("PlayerState_Kill Enter");            
            _model.DOMove(_model.movePosition, _moveTime, OnMoveComplete);
        }

        void IState.Exit()
        {
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

        private void OnMoveComplete()
        {
            _fsm.ChangeState<PlayerState_Idle>();
            _enemyTurnCommand.Execute();
        }

        public class Factory : Factory<PlayerState_Kill>
        { }
    }
}