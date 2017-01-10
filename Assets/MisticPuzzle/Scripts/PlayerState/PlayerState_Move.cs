using Zenject;

namespace Lonely
{
    public class PlayerState_Move : IState
    {
        #region Explicit Interface

        void IState.Enter()
        {
            _model.DOMove(_model.movePosition, _moveTime, OnMoveComplete);
        }

        void IState.Exit()
        {
        }

        #endregion Explicit Interface

        private readonly PlayerFSM _fsm;
        private readonly GameCommands.EnemyTurn _enemyTurnCommand;
        private readonly float _moveTime;
        private readonly PlayerModel _model;

        public PlayerState_Move(PlayerFSM fsm, PlayerModel model, GameCommands.EnemyTurn enemyTurnCommand,
                                float moveTime)
        {
            _fsm = fsm;
            _model = model;
            _enemyTurnCommand = enemyTurnCommand;
            _moveTime = moveTime;
        }

        private void OnMoveComplete()
        {
            _fsm.ChangeState<PlayerState_Idle>();
            _enemyTurnCommand.Execute();
        }

        public class Factory : Factory<PlayerState_Move>
        {
        }
    }
}