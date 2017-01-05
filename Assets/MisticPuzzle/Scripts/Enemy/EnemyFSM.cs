using Extension;
using System;
using System.Collections.Generic;
using Zenject;

namespace Lonely
{
    public class EnemyFSM : IInitializable, IDisposable
    {
        #region Explicit Interface

        void IDisposable.Dispose()
        {
            _fsm.Dispose();
        }

        void IInitializable.Initialize()
        {
            var states = new List<IState>() { _idleFactory.Create(),
                                              _moveTargetFactory.Create(),
                                              _returnFactory.Create(),
                                              _killFactory.Create(),
                                              _dieFactory.Create() };
            _fsm = _fsmFactory.Create(states);

            _fsm.Initialize();
        }

        #endregion Explicit Interface

        private IFSM _fsm = FSM.Null;
        private readonly FSM.Factory _fsmFactory;
        private readonly EnemyState_Idle.Factory _idleFactory;
        private readonly EnemyState_MoveToTarget.Factory _moveTargetFactory;
        private readonly EnemyState_Return.Factory _returnFactory;
        private readonly EnemyState_Kill.Factory _killFactory;
        private readonly EnemyState_Die.Factory _dieFactory;

        public IState curState { get { return _fsm.curState; } }

        public EnemyFSM(FSM.Factory fsmFactory,
                        EnemyState_Idle.Factory idleFactory,
                        EnemyState_MoveToTarget.Factory moveTargetFactory,
                        EnemyState_Return.Factory returnFactory,
                        EnemyState_Kill.Factory killFactory,
                        EnemyState_Die.Factory dieFactory)
        {
            _fsmFactory = fsmFactory;
            _idleFactory = idleFactory;
            _moveTargetFactory = moveTargetFactory;
            _returnFactory = returnFactory;
            _killFactory = killFactory;
            _dieFactory = dieFactory;
        }

        public void ChangeState<TState>() where TState : class, IState
        {
            _fsm.ChangeState<TState>();
        }

        public void EnemyTurn()
        {
            var turnable = _fsm.curState as ITurnable;
            if (turnable.IsValid())
                turnable.Turn();
        }
    }
}