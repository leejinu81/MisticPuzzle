using Extension;
using UnityEngine;
using Zenject;

namespace Lonely
{
    public class EnemyState_MoveToTarget : IState, ITurnable, ITitanShield
    {
        #region Explicit Interface

        void IState.Enter()
        {
            Debug.Log("EnemyState_MoveToTarget Enter");
            _model.DOSpriteFade(Color.red, _moveTime, () => _playerTurn.Execute());
            _model.enableTarget = true;
        }

        void IState.Exit()
        {
            _model.spriteColor = Color.white;
        }

        void ITurnable.Turn()
        {
            Player player;
            if (_eye.Look(out player))
            {
                _model.targetPos = player.XY();
            }

            Move(_model.dir);
        }

        #endregion Explicit Interface

        private readonly EnemyFSM _fsm;
        private readonly EnemyModel _model;
        private readonly LayerMask _blockLayer;
        private readonly EnemyEye _eye;
        private readonly float _moveTime;
        private readonly GameCommands.PlayerTurn _playerTurn;

        public EnemyState_MoveToTarget(EnemyFSM fsm, EnemyModel model, LayerMask blockLayer, EnemyEye eye, float moveTime, GameCommands.PlayerTurn playerTurn)
        {
            _fsm = fsm;
            _model = model;
            _blockLayer = blockLayer;
            _eye = eye;
            _moveTime = moveTime;
            _playerTurn = playerTurn;
        }

        private void Move(Vector2 dir)
        {
            var startPos = _model.position;
            var endPos = startPos + dir;

            var hitInfo = Linecast(startPos, endPos);
            if (hitInfo.transform.IsNull())
            {
                _model.DOMove(_model.position + dir, _moveTime, OnMoveComplete);
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

            _model.targetPos = player.XY();
            _fsm.ChangeState<EnemyState_Kill>();
        }

        private RaycastHit2D Linecast(Vector2 start, Vector2 end)
        {
            _model.enableCollider = false;
            var hitInfo = Physics2D.Linecast(start, end, _blockLayer);
            _model.enableCollider = true;

            return hitInfo;
        }

        private void OnMoveComplete()
        {
            if (Equals(_model.position, _model.targetPos))
            {
                _fsm.ChangeState<EnemyState_Return>();
            }

            _playerTurn.Execute();
        }

        public class Factory : Factory<EnemyState_MoveToTarget>
        { }
    }

    public interface ITitanShield
    {
    }
}