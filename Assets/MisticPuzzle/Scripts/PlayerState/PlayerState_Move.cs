using Zenject;

namespace Lonely
{
    public class PlayerState_Move : State
    {
        public PlayerState_Move(IStateEnter enter, IStateExit exit, IStateUpdate update)
            : base(enter, exit, update)
        {
        }

        public class CustomFactory : IFactory<State>
        {
            #region interface

            State IFactory<State>.Create()
            {
                var binder = StateBinder<PlayerState_Move>.For(_container);
                return binder.Enter<PlayerStateMove_Enter>().Make();
            }

            #endregion interface

            private readonly DiContainer _container;

            public CustomFactory(DiContainer container)
            {
                _container = container;
            }
        }
    }

    public class PlayerStateMove_Enter : IStateEnter
    {
        #region Explicit Interface

        void IStateEnter.Enter()
        {
            _model.DOMove(_model.movePosition, _moveTime, OnMoveComplete);
        }

        #endregion Explicit Interface

        private readonly PlayerFSM _fsm;

        private readonly GameCommands.EnemyTurn _enemyTurnCommand;
        private readonly float _moveTime;
        private readonly PlayerModel _model;

        public PlayerStateMove_Enter(PlayerFSM fsm, PlayerModel model, GameCommands.EnemyTurn enemyTurnCommand,
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
    }
}