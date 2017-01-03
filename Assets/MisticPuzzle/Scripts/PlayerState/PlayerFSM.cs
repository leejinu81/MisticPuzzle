using System;
using System.Collections.Generic;
using Zenject;

namespace Lonely
{
    public class PlayerFSM : IFSM
    {
        #region Explicit Interface

        float IFSM.stateTime { get { return _fsm.stateTime; } }

        void IFSM.ChangeState<TState>()
        {
            _fsm.ChangeState<TState>();
        }

        bool IFSM.IsCurState<TState>()
        {
            return _fsm.IsCurState<TState>();
        }

        bool IFSM.IsPrevState<TState>()
        {
            return _fsm.IsPrevState<TState>();
        }

        void IInitializable.Initialize()
        {
            var states = new List<IState>() { _idleStateFactory.Create(),
                                              _moveStateFactory.Create()  };
            _fsm = _fsmFactory.Create(states);

            _fsm.Initialize();
        }

        void ITickable.Tick()
        {
            _fsm.Tick();
        }

        void IFixedTickable.FixedTick()
        {
            _fsm.FixedTick();
        }

        void ILateTickable.LateTick()
        {
            _fsm.LateTick();
        }

        void IDisposable.Dispose()
        {
            _fsm.Dispose();
        }

        #endregion Explicit Interface

        private IFSM _fsm = FSM.Null;
        private readonly FSM.Factory _fsmFactory;
        private readonly PlayerState_Idle.Factory _idleStateFactory;
        private readonly PlayerState_Move.Factory _moveStateFactory;

        public PlayerFSM(FSM.Factory fsmFactory, PlayerState_Idle.Factory idleStateFactory,
                                                 PlayerState_Move.Factory moveStateFactory)
        {
            _fsmFactory = fsmFactory;
            _idleStateFactory = idleStateFactory;
            _moveStateFactory = moveStateFactory;
        }
    }
}