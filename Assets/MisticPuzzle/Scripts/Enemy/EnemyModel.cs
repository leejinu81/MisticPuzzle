using DG.Tweening;
using UnityEngine;

namespace Lonely
{
    public class EnemyModel
    {
        private readonly Transform _transform;
        private readonly Collider2D _collider2D;
        private readonly Vector2 _originPos;
        private readonly eDirection _originDir;
        private readonly GameObject _go, _targetPoint;
        private readonly SpriteRenderer _sprite;

        private Vector2 _dir;

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

        public Vector2 originPos
        {
            get { return _originPos; }
        }

        public bool enableTarget
        {
            get { return _targetPoint.activeInHierarchy; }
            set { _targetPoint.SetActive(value); }
        }

        public Vector2 targetPos
        {
            get { return _targetPoint.transform.position; }
            set { _targetPoint.transform.position = value; }
        }

        public bool enableGameObject
        {
            get { return _go.activeInHierarchy; }
            set { _go.SetActive(value); }
        }

        public Color spriteColor
        {
            get { return _sprite.color; }
            set { _sprite.color = value; }
        }

        public EnemyModel(Transform transform, Collider2D collider2D, GameObject go, SpriteRenderer sprite,
                          eDirection originDir, TargetPositionFactory targetFactory)
        {
            _transform = transform;
            _collider2D = collider2D;
            _go = go;
            _sprite = sprite;
            _originDir = originDir;
            _targetPoint = targetFactory.Create();

            SetDirection(dir);

            _originPos = _transform.position;
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
                //_transform.localScale = new Vector3(-1, 1, 1);
                _transform.localScale = Vector3.one;
            }
            else if (Equals(_dir, Vector2.right))
            {
                //_dir = eDirection.right;
                //_transform.localScale = Vector3.one;
                _transform.localScale = new Vector3(-1, 1, 1);
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

        public void ResetDirection()
        {
            SetDirection(_originDir);
        }

        public void DOMove(Vector3 endValue, float duration, TweenCallback complete = null)
        {
            var delta = endValue - _transform.position;
            SetDirection(delta);

            _transform.DOMove(endValue, duration).SetEase(Ease.OutQuint).OnComplete(complete);
        }

        public void DOSpriteFade(Color endColor, float duration, TweenCallback complete = null)
        {
            _sprite.DOColor(endColor, duration).SetEase(Ease.Linear).OnComplete(complete);
        }
    }
}