using DG.Tweening;
using Extension;
using Zenject;

namespace Lonely
{
    public class PlayerState_Block : IState, ITickable
    {
        #region Explicit Interface

        void IState.Enter()
        {
            //_model.DOMove(_model.movePosition, _moveTime);            
            _model.DOBlock();
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
        private readonly PlayerModel _model;
        private readonly float _moveTime;

        public PlayerState_Block(PlayerFSM fsm, PlayerModel model, float moveTime)
        {
            _fsm = fsm;
            _model = model;
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