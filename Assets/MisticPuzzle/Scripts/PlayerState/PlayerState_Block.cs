using Extension;
using Zenject;

namespace Lonely
{
    public class PlayerState_Block : State
    {
        public PlayerState_Block(IStateEnter enter, IStateExit exit, IStateUpdate update)
            : base(enter, exit, update)
        {
        }

        public class CustomFactory : IFactory<State>
        {
            #region interface

            State IFactory<State>.Create()
            {
                var binder = StateBinder<PlayerState_Block>.For(_container);
                return binder.Enter<PlayerStateBlock_Enter>().Update<PlayerStateBlock_Update>().Make();
            }

            #endregion interface

            private readonly DiContainer _container;

            public CustomFactory(DiContainer container)
            {
                _container = container;
            }
        }
    }

    public class PlayerStateBlock_Enter : IStateEnter
    {
        void IStateEnter.Enter()
        {
            _model.DOBlock();
        }

        private readonly PlayerModel _model;

        public PlayerStateBlock_Enter(PlayerModel model)
        {
            _model = model;
        }
    }

    public class PlayerStateBlock_Update : IStateUpdate
    {
        void IStateUpdate.Update()
        {
            if (IsOverMoveTime())
            {
                _fsm.ChangeState<PlayerState_Idle>();
            }
        }

        private readonly PlayerFSM _fsm;
        private readonly float _moveTime;

        public PlayerStateBlock_Update(PlayerFSM fsm, float moveTime)
        {
            _fsm = fsm;
            _moveTime = moveTime;
        }

        private bool IsOverMoveTime()
        {
            return _fsm.stateTime.IsGreater(_moveTime);
        }
    }
}