using System;
using System.Collections.Generic;
using Zenject;

namespace Lonely
{
    public class PlayerFSM : IInitializable, ITickable, IDisposable
    {
        #region Explicit Interface

        void IInitializable.Initialize()
        {
            var states = new List<IState>() { _idleFactory.Create(),
                                              _moveFactory.Create(),
                                              _blockFactory.Create(),
                                              _killFactory.Create(),
                                              _dieFactory.Create() };
            _fsm = _fsmFactory.Create(states);

            _fsm.Initialize();
        }

        void ITickable.Tick()
        {
            _fsm.Tick();
        }

        void IDisposable.Dispose()
        {
            _fsm.Dispose();
        }

        #endregion Explicit Interface

        private IFSM _fsm = FSM.Null;
        private readonly FSM.Factory _fsmFactory;
        private readonly PlayerState_Idle.Factory _idleFactory;
        private readonly PlayerState_Move.Factory _moveFactory;
        private readonly PlayerState_Block.Factory _blockFactory;
        private readonly PlayerState_Kill.Factory _killFactory;
        private readonly PlayerState_Die.Factory _dieFactory;

        public float stateTime { get { return _fsm.stateTime; } }

        public PlayerFSM(FSM.Factory fsmFactory,
                         PlayerState_Idle.Factory idleFactory,
                         PlayerState_Move.Factory moveFactory,
                         PlayerState_Block.Factory blockFactory,
                         PlayerState_Kill.Factory killFactory,
                         PlayerState_Die.Factory dieFactory)
        {
            _fsmFactory = fsmFactory;
            _idleFactory = idleFactory;
            _moveFactory = moveFactory;
            _blockFactory = blockFactory;
            _killFactory = killFactory;
            _dieFactory = dieFactory;
        }

        public void ChangeState<TState>() where TState : class, IState
        {
            _fsm.ChangeState<TState>();
        }

        public void PlayerTurn()
        {
            _fsm.ChangeState<PlayerState_Idle>();
        }
    }
}