using Extension;
using Zenject;

namespace Lonely
{
    public class PlayerState_Block : IState, ITickable
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
            if (IsOverMoveTime())
            {
                _fsm.ChangeState<PlayerState_Idle>();
            }
        }

        #endregion Explicit Interface

        private readonly PlayerFSM _fsm;
        private readonly float _moveTime;

        public PlayerState_Block(PlayerFSM fsm, float moveTime)
        {
            _fsm = fsm;
            _moveTime = moveTime;
        }

        private bool IsOverMoveTime()
        {
            return _fsm.stateTime.IsGreater(_moveTime);
        }

        public class Factory : Factory<PlayerState_Block>
        { }
    }
}