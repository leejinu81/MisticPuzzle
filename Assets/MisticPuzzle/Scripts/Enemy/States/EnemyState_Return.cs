using Extension;
using UnityEngine;
using Zenject;

namespace Lonely
{
    public class EnemyState_Return : EnemyState
    {
        public EnemyState_Return(IStateEnter enter, IStateExit exit, IStateUpdate update,
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
                                  .Enter<EnemyStateReturn_Enter>()
                                  .Exit<EnemyStateReturn_Exit>()
                                  .Turn<EnemyStateReturn_Turn>()
                                  .Shield<TitanShield>()
                                  .Make<EnemyState_Return>();
            }

            #endregion interface

            private readonly DiContainer _container;

            public CustomFactory(DiContainer container)
            {
                _container = container;
            }
        }
    }

    public class EnemyStateReturn_Enter : IStateEnter
    {
        void IStateEnter.Enter()
        {
            _model.spriteColor = Color.red;
            _model.enableTarget = false;
            SetReturnDirection();
        }

        private readonly EnemyModel _model;

        public EnemyStateReturn_Enter(EnemyModel model)
        {
            _model = model;
        }

        private void SetReturnDirection()
        {
            var dirToReturn = (_model.originPos - _model.position).normalized;
            _model.SetDirection(dirToReturn);
        }
    }

    public class EnemyStateReturn_Exit : IStateExit
    {
        void IStateExit.Exit()
        {
            _model.spriteColor = Color.white;
        }

        private readonly EnemyModel _model;

        public EnemyStateReturn_Exit(EnemyModel model)
        {
            _model = model;
        }
    }

    public class EnemyStateReturn_Turn : ITurnable
    {
        void ITurnable.Turn()
        {
            Move(_model.dir);
        }

        private readonly GuardianFSM _fsm;
        private readonly EnemyModel _model;
        private readonly float _moveTime;
        private readonly LayerMask _blockLayer;
        private readonly GameCommands.PlayerTurn _playerTurn;

        public EnemyStateReturn_Turn(GuardianFSM fsm, EnemyModel model, float moveTime, LayerMask blockLayer, GameCommands.PlayerTurn playerTurn)
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
                //_model.position = _model.position + dir;
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
            if (Equals(_model.position, _model.originPos))
            {
                _fsm.ChangeState<EnemyState_Idle>();
            }

            _playerTurn.Execute();
        }
    }
}