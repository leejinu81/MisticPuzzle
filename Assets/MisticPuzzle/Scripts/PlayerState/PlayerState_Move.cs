﻿using Extension;
using UnityEngine;
using Zenject;

namespace Lonely
{
    public class PlayerState_Move : IState, ITickable
    {
        #region Explicit Interface

        void IState.Enter()
        {
            Debug.Log("PlayerState_Move Enter");
            _model.position = _model.movePosition;
            _enemyTurnCommand.Execute();
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

        private readonly PlayerFSM _fsm;
        private readonly GameCommands.EnemyTurn _enemyTurnCommand;
        private readonly float _moveTime;
        private readonly PlayerModel _model;

        public PlayerState_Move(PlayerFSM fsm, PlayerModel model, GameCommands.EnemyTurn enemyTurnCommand,
                                float moveTime)
        {
            _fsm = fsm;
            _model = model;
            _enemyTurnCommand = enemyTurnCommand;
            _moveTime = moveTime;
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