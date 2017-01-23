using Extension;
using UnityEngine;
using Zenject;

namespace Lonely
{
    public class EnemyState_MoveToTarget : GuardianState, ITitanShield
    {
        public EnemyState_MoveToTarget(IStateEnter enter, IStateExit exit, IStateUpdate update, ITurnable turnable)
            : base(enter, exit, update, turnable)
        {
        }

        public class CustomFactory : IFactory<GuardianState>
        {
            #region interface

            GuardianState IFactory<GuardianState>.Create()
            {
                var binder = GuardianStateBinder<EnemyState_MoveToTarget>.For(_container);
                return binder.Turn<EnemyStateMoveToTarget_Turn>()
                             .Enter<EnemyStateMoveToTarget_Enter>()
                             .Exit<EnemyStateMoveToTarget_Exit>().Make();
            }

            #endregion interface

            private readonly DiContainer _container;

            public CustomFactory(DiContainer container)
            {
                _container = container;
            }
        }
    }

    public class EnemyStateMoveToTarget_Enter : IStateEnter
    {
        void IStateEnter.Enter()
        {
            Debug.Log("EnemyState_MoveToTarget Enter");
            _model.DOSpriteFade(Color.red, _moveTime, () => _playerTurn.Execute());
            _model.enableTarget = true;
        }

        private readonly EnemyModel _model;
        private readonly float _moveTime;
        private readonly GameCommands.PlayerTurn _playerTurn;

        public EnemyStateMoveToTarget_Enter(EnemyModel model, float moveTime, GameCommands.PlayerTurn playerTurn)
        {
            _model = model;
            _moveTime = moveTime;
            _playerTurn = playerTurn;
        }
    }

    public class EnemyStateMoveToTarget_Exit : IStateExit
    {
        void IStateExit.Exit()
        {
            _model.spriteColor = Color.white;
        }

        private readonly EnemyModel _model;

        public EnemyStateMoveToTarget_Exit(EnemyModel model)
        {
            _model = model;
        }
    }

    public class EnemyStateMoveToTarget_Turn : ITurnable
    {
        void ITurnable.Turn()
        {
            Player player;
            if (_eye.Look(out player))
            {
                _model.targetPos = player.XY();
            }

            Move(_model.dir);
        }

        private readonly GuardianFSM _fsm;
        private readonly EnemyEye _eye;
        private readonly EnemyModel _model;
        private readonly float _moveTime;
        private readonly LayerMask _blockLayer;
        private readonly GameCommands.PlayerTurn _playerTurn;

        public EnemyStateMoveToTarget_Turn(GuardianFSM fsm, EnemyEye eye, EnemyModel model, float moveTime, LayerMask blockLayer, GameCommands.PlayerTurn playerTurn)
        {
            _fsm = fsm;
            _eye = eye;
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
    }

    public interface ITitanShield
    {
    }
}