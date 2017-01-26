using Extension;
using UnityEngine;
using Zenject;

namespace Lonely
{
    public class EnemyState_Patrol : EnemyState
    {
        public EnemyState_Patrol(IStateEnter enter, IStateExit exit, IStateUpdate update,
                                     ITurnable turnable, ITitanShield titanShield)
            : base(enter, exit, update, turnable, titanShield)
        {
        }

        public class CustomFactory : IFactory<EnemyState>
        {
            #region interface

            EnemyState IFactory<EnemyState>.Create()
            {
                return StateBinder.Bind(_container)
                                  .Enter<EnemyStatePatrol_Enter>()
                                  .Turn<EnemyStatePatrol_Turn>()
                                  .Make<EnemyState_Patrol>();
            }

            #endregion interface

            private readonly DiContainer _container;

            public CustomFactory(DiContainer container)
            {
                _container = container;
            }
        }
    }

    public class EnemyStatePatrol_Enter : IStateEnter
    {
        void IStateEnter.Enter()
        {
            _model.enableTarget = false;
        }

        private readonly EnemyModel _model;

        public EnemyStatePatrol_Enter(EnemyModel model)
        {
            _model = model;
        }
    }

    public class EnemyStatePatrol_Turn : ITurnable
    {
        void ITurnable.Turn()
        {
            Move(_model.dir);
        }

        private readonly PatrolEnemyFSM _fsm;
        private readonly EnemyModel _model;
        private readonly float _moveTime;
        private readonly LayerMask _blockLayer;
        private readonly GameCommands.PlayerTurn _playerTurn;

        public EnemyStatePatrol_Turn(PatrolEnemyFSM fsm, EnemyModel model, float moveTime, LayerMask blockLayer, GameCommands.PlayerTurn playerTurn)
        {
            _fsm = fsm;
            _model = model;
            _moveTime = moveTime;
            _blockLayer = blockLayer;
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
            else if (hitInfo.transform.CompareTag("Wall"))
            {
                _model.SetDirection(-_model.dir);
                _model.DOMove(_model.position + _model.dir, _moveTime, OnMoveComplete);
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
            _playerTurn.Execute();
        }
    }
}