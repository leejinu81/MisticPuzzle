using Extension;
using UnityEngine;
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
                return StateBinder.Bind(_container)
                                  .Enter<PlayerStateMove_Enter>(typeof(BreakStepOnFloor))
                                  .Make<PlayerState_Move>();
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
            _decorated.Enter();
        }

        #endregion Explicit Interface

        private readonly PlayerFSM _fsm;

        private readonly GameCommands.EnemyTurn _enemyTurnCommand;
        private readonly float _moveTime;
        private readonly PlayerModel _model;
        private readonly IStateEnter _decorated;

        public PlayerStateMove_Enter(PlayerFSM fsm, PlayerModel model, GameCommands.EnemyTurn enemyTurnCommand,
                                float moveTime, IStateEnter decorated)
        {
            _fsm = fsm;
            _model = model;
            _enemyTurnCommand = enemyTurnCommand;
            _moveTime = moveTime;
            _decorated = decorated;
        }

        private void OnMoveComplete()
        {
            _model.position = _model.movePosition;
            _fsm.ChangeState<PlayerState_Idle>();
            _enemyTurnCommand.Execute();
        }
    }

    public class BreakStepOnFloor : IStateEnter
    {
        #region Explicit Interface

        void IStateEnter.Enter()
        {
            if (_model.stepOnFloor.IsValid())
            {
                var breakableFloor = _model.stepOnFloor.GetComponent<BreakableFloor>();
                Debug.Assert(breakableFloor.IsValid());

                breakableFloor.Break();
                _model.stepOnFloor = null;
            }
        }

        #endregion Explicit Interface

        private readonly PlayerModel _model;

        public BreakStepOnFloor(PlayerModel model)
        {
            _model = model;
        }
    }
}