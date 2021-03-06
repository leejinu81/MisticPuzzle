﻿namespace Lonely
{
    public class PatrolEnemy : Enemy
    {
        public override bool hasTitanSheild { get { return _fsm.hasTitanSheild; } }

        private PatrolEnemyFSM _fsm;

        public PatrolEnemy(PatrolEnemyFSM fsm) : base()
        {
            _fsm = fsm;
        }

        public override void Die()
        {
            _fsm.ChangeState<EnemyState_Die>();
        }

        public override void Turn()
        {
            _fsm.EnemyTurn();
        }

        protected override void Initialize()
        {
            _fsm.ChangeState<EnemyState_Patrol>();
        }
    }
}