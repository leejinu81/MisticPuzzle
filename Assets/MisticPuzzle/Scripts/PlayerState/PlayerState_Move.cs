using Extension;
using UnityEngine;
using Zenject;

namespace Lonely
{
    public class PlayerState_Move : IState, ITickable
    {
        #region Explicit Interface

        void IState.Enter()
        {
            var h = ClampHorizontal(_input.horizontal);
            var v = ClampVertical(_input.vertical);
            Move(h, v);
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

        private readonly IFSM _fsm;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly Collider2D _collider2D;
        private readonly MisticPuzzleInput _input;
        private readonly float _moveTime;
        private readonly LayerMask _blockLayer;

        public PlayerState_Move(IFSM fsm, Rigidbody2D rigidbody2D, Collider2D collider2D, MisticPuzzleInput input, float moveTime, LayerMask blockLayer)
        {
            _fsm = fsm;
            _rigidbody2D = rigidbody2D;
            _collider2D = collider2D;
            _input = input;
            _moveTime = moveTime;
            _blockLayer = blockLayer;
        }

        private int ClampHorizontal(int horizontal)
        {
            return Mathf.Clamp(horizontal, -1, 1);
        }

        private int ClampVertical(int vertical)
        {
            return Mathf.Clamp(vertical, -1, 1);
        }

        private bool CanMove(int horizontal, int vertical)
        {
            var startPos = _rigidbody2D.position;
            var endPos = startPos + new Vector2(horizontal, vertical);

            _collider2D.enabled = false;
            var hitInfo = Physics2D.Linecast(startPos, endPos, _blockLayer);
            _collider2D.enabled = true;

            return hitInfo.transform == null;
        }

        private void Move(int horizontal, int vertical)
        {
            if (CanMove(horizontal, vertical))
            {
                var newPos = _rigidbody2D.position + new Vector2(horizontal, vertical);
                _rigidbody2D.MovePosition(newPos);
            }
        }

        private bool IsOverMoveTime()
        {
            return _fsm.stateTime.IsGreater(_moveTime);
        }

        public class Factory : Factory<PlayerState_Move>
        {
        }
    }
}