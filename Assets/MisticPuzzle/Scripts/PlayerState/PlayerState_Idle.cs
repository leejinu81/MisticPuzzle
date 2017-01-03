using Extension;
using UnityEngine;
using Zenject;

namespace Lonely
{
    public class PlayerState_Idle : IState, ITickable
    {
        #region Explicit Interface

        void IState.Enter()
        {            
        }

        void IState.Exit()
        {            
        }

        void ITickable.Tick()
        {
            if (Equals(_input.horizontal, 0).IsFalse() ||
                    Equals(_input.vertical, 0).IsFalse())
            {
                _fsm.ChangeState<PlayerState_Move>();
            }
        }

        #endregion Explicit Interface

        private readonly IFSM _fsm;
        private readonly MisticPuzzleInput _input;

        public PlayerState_Idle(IFSM fsm, MisticPuzzleInput input)
        {
            _fsm = fsm;
            _input = input;
        }

        public class Factory : Factory<PlayerState_Idle>
        {
        }
    }
}