using Extension;
using System;
using System.Collections.Generic;
using Zenject;

namespace Lonely
{
    public class PatrolEnemyFSM : IInitializable, IDisposable
    {
        #region Explicit Interface

        void IInitializable.Initialize()
        {
            //var states = new List<IState>() { _patrolFactory.Create(),
            //                                  _killFactory.Create(),
            //                                  _dieFactory.Create() };
            //_fsm = _fsmFactory.Create(states);

            //_fsm.Initialize();
        }

        void IDisposable.Dispose()
        {
           // _fsm.Dispose();
        }

        #endregion Explicit Interface

        //private IFSM _fsm = FSM.Null;
        //private readonly FSM.Factory _fsmFactory;
        //private readonly EnemyState_Patrol.Factory _patrolFactory;
        //private readonly EnemyState_Kill.Factory _killFactory;
        //private readonly EnemyState_Die.Factory _dieFactory;

        //public PatrolEnemyFSM(FSM.Factory fsmFactory, EnemyState_Patrol.Factory patrolFactory,
        //                                              EnemyState_Kill.Factory killFactory,
        //                                              EnemyState_Die.Factory dieFactory)
        //{
        //    _fsmFactory = fsmFactory;
        //    _patrolFactory = patrolFactory;
        //    _killFactory = killFactory;
        //    _dieFactory = dieFactory;
        //}

        //public void ChangeState<TState>() where TState : class, IState
        //{
        //    _fsm.ChangeState<TState>();
        //}

        //public void EnemyTurn()
        //{
        //    var turnable = _fsm.curState as ITurnable;
        //    if (turnable.IsValid())
        //        turnable.Turn();
        //}
    }
        
}