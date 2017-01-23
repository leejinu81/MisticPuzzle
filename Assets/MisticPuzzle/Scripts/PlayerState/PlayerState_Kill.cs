using UnityEngine;
using Zenject;

namespace Lonely
{
    public class PlayerState_Kill : State
    {
        public PlayerState_Kill(IStateEnter enter, IStateExit exit, IStateUpdate update)
            : base(enter, exit, update)
        {
        }

        public class CustomFactory : IFactory<State>
        {
            #region interface

            State IFactory<State>.Create()
            {
                var binder = StateBinder<PlayerState_Kill>.For(_container);
                return binder.Enter<PlayerStateKill_Enter>().Make();
            }

            #endregion interface

            private readonly DiContainer _container;

            public CustomFactory(DiContainer container)
            {
                _container = container;
            }
        }
    }

    public class PlayerStateKill_Enter : IStateEnter
    {
        void IStateEnter.Enter()
        {
            Debug.Log("PlayerState_Kill Enter");
            _model.DOMove(_model.movePosition, _moveTime, OnMoveComplete);
        }

        private readonly PlayerFSM _fsm;
        private readonly PlayerModel _model;
        private readonly float _moveTime;
        private readonly GameCommands.EnemyTurn _enemyTurnCommand;

        public PlayerStateKill_Enter(PlayerFSM fsm, PlayerModel model, float moveTime, GameCommands.EnemyTurn enemyTurnCommand)
        {
            _fsm = fsm;
            _model = model;
            _moveTime = moveTime;
            _enemyTurnCommand = enemyTurnCommand;
        }

        private void OnMoveComplete()
        {
            _fsm.ChangeState<PlayerState_Idle>();
            _enemyTurnCommand.Execute();
        }
    }
}