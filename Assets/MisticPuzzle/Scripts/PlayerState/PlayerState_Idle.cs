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
            Debug.Log("PlayerState_Idle Enter");
        }

        void IState.Exit()
        {
        }

        void ITickable.Tick()
        {
            if (Equals(_input.horizontal, 0).IsFalse() || Equals(_input.vertical, 0).IsFalse())
            {
                Move(ClampHorizontal(_input.horizontal), ClampVertical(_input.vertical));
            }
        }

        #endregion Explicit Interface

        private readonly PlayerFSM _fsm;
        private readonly MisticPuzzleInput _input;
        private readonly PlayerModel _model;
        private readonly LayerMask _blockLayer;

        public PlayerState_Idle(PlayerFSM fsm, PlayerModel model, MisticPuzzleInput input, LayerMask blockLayer)
        {
            _fsm = fsm;
            _input = input;
            _model = model;
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

        private void Move(int horizontal, int vertical)
        {
            var startPos = _model.position;
            var endPos = startPos + new Vector2(horizontal, vertical);

            var hitInfo = Linecast(startPos, endPos);
            if (hitInfo.transform.IsNull())
            {
                _model.movePosition = endPos;
                _fsm.ChangeState<PlayerState_Move>();
            }
            else if (hitInfo.transform.CompareTag("Enemy"))
            {
                var enemy = hitInfo.transform.GetComponent<Enemy>();
                Debug.Assert(enemy.IsValid());

                MoveToEnemy(enemy);
            }
        }

        private void MoveToEnemy(Enemy enemy)
        {
            if (enemy.isTitanShield)
            {
                _fsm.ChangeState<PlayerState_Block>();
            }
            else
            {
                enemy.Die();
                _model.movePosition = enemy.XY();
                _fsm.ChangeState<PlayerState_Kill>();
            }
        }

        private RaycastHit2D Linecast(Vector2 start, Vector2 end)
        {
            _model.enableCollider = false;
            var hitInfo = Physics2D.Linecast(start, end, _blockLayer);
            _model.enableCollider = true;

            return hitInfo;
        }

        public class Factory : Factory<PlayerState_Idle>
        {
        }
    }
}