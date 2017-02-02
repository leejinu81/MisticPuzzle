using DG.Tweening;
using UnityEngine;

namespace Lonely
{
    public enum eDirection
    {
        left,
        right,
        up,
        down
    }

    public class PlayerModel
    {
        //private eDirection _dir;

        private readonly Collider2D _collider2D;
        private readonly Transform _transform;
        private readonly GameObject _go;

        //public eDirection dir { get { return _dir; } }
        private Vector2 _dir, _movePosition;
        private Collider2D _stepOnFloor;

        public Vector2 dir { get { return _dir; } }

        public Vector2 position
        {
            get
            {
                return _transform.position;
            }
            set
            {
                var delta = value - position;
                SetDirection(delta);
                _transform.position = value;
            }
        }

        public bool enableCollider
        {
            get { return _collider2D.enabled; }
            set { _collider2D.enabled = value; }
        }

        public Vector2 movePosition
        {
            get { return _movePosition; }
            set { _movePosition = value; }
        }

        public bool enableGameObject
        {
            get { return _go.activeInHierarchy; }
            set { _go.SetActive(value); }
        }

        public Collider2D stepOnFloor
        {
            get { return _stepOnFloor; }
            set { _stepOnFloor = value; }
        }

        public PlayerModel(Transform transform, Collider2D collider2D, GameObject go)
        {
            _transform = transform;
            _collider2D = collider2D;
            _go = go;
        }

        private void SetDirection(eDirection dir)
        {
            switch (dir)
            {
                case eDirection.left:
                    _dir = Vector2.left;
                    break;

                case eDirection.right:
                    _dir = Vector2.right;
                    break;

                case eDirection.up:
                    _dir = Vector2.up;
                    break;

                case eDirection.down:
                    _dir = Vector2.down;
                    break;

                default:
                    break;
            }

            SetDirection();
        }

        public void SetDirection(Vector2 dir)
        {
            _dir = dir.normalized;
            SetDirection();
        }

        private void SetDirection()
        {
            if (Equals(_dir, Vector2.left))
            {
                //_dir = eDirection.left;
                _transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (Equals(_dir, Vector2.right))
            {
                //_dir = eDirection.right;
                _transform.localScale = Vector3.one;
            }
            else if (Equals(_dir, Vector2.up)) // FIXME : up, down Sprite가 필요!!
            {
                //_dir = eDirection.up;
            }
            else if (Equals(_dir, Vector2.down))
            {
                //_dir = eDirection.down;
            }
        }

        public void DOMove(Vector3 endValue, float duration, TweenCallback complete = null)
        {
            var delta = endValue - _transform.position;
            SetDirection(delta);

            _transform.DOMove(endValue, duration).SetEase(Ease.OutQuint).OnComplete(complete);
        }

        public void DOBlock()
        {
            var seq = DOTween.Sequence();
                        
            Vector2 pos = _transform.position;
            var dir = (_movePosition - pos).normalized;
            var moveDist = pos + (dir * 0.2f);
            var moveTween = _transform.DOMove(moveDist, 0.1f).SetEase(Ease.OutQuint);

            var blockTween = _transform.DOMove(pos, 0.2f).SetEase(Ease.OutBack);
            seq.Append(moveTween);
            seq.Append(blockTween);
        }
    }
}