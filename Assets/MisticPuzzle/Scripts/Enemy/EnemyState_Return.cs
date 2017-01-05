﻿using Extension;
using UnityEngine;
using Zenject;

namespace Lonely
{
    public class EnemyState_Return : IState, ITurnable, ITitanShield
    {
        #region Explicit Interface

        void IState.Enter()
        {
            Debug.Log("EnemyState_Return Enter");
            _model.spriteColor = Color.red;
            _model.enableTarget = false;
            SetReturnDirection();
        }

        void IState.Exit()
        {
            _model.spriteColor = Color.white;
        }

        void ITurnable.Turn()
        {
            Move(_model.dir);
            if (Equals(_model.position, _model.originPos))
            {
                _fsm.ChangeState<EnemyState_Idle>();
            }
        }

        #endregion Explicit Interface

        private readonly EnemyFSM _fsm;
        private readonly EnemyModel _model;
        private readonly LayerMask _blockLayer;

        public EnemyState_Return(EnemyFSM fsm, EnemyModel model, LayerMask blockLayer)
        {
            _fsm = fsm;
            _model = model;
            _blockLayer = blockLayer;
        }

        private void SetReturnDirection()
        {
            var dirToReturn = (_model.originPos - _model.position).normalized;
            _model.SetDirection(dirToReturn);
        }

        private void Move(Vector2 dir)
        {
            var startPos = _model.position;
            var endPos = startPos + dir;

            var hitInfo = Linecast(startPos, endPos);
            if (hitInfo.transform.IsNull())
            {
                _model.position = _model.position + dir;
            }
            else if (hitInfo.transform.CompareTag("Player"))
            {
                var player = hitInfo.transform.GetComponent<Player>();
                Debug.Assert(player.IsValid());

                PlayerKill(player);
            }
        }

        private void PlayerKill(Player player)
        {
            player.Die();

            _model.position = player.XY();
            _fsm.ChangeState<EnemyState_Kill>();
        }

        private RaycastHit2D Linecast(Vector2 start, Vector2 end)
        {
            _model.enableCollider = false;
            var hitInfo = Physics2D.Linecast(start, end, _blockLayer);
            _model.enableCollider = true;

            return hitInfo;
        }

        public class Factory : Factory<EnemyState_Return>
        { }
    }
}