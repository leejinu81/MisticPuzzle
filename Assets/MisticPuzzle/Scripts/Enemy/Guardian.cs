namespace Lonely
{
    public class Guardian : Enemy
    {
        public override bool hasTitanSheild { get { return _fsm.hasTitanSheild; } }

        private readonly GuardianFSM _fsm;

        public Guardian(GuardianFSM fsm) : base()
        {
            _fsm = fsm;
        }

        protected override void Initialize()
        {
            _fsm.ChangeState<EnemyState_Idle>();
        }

        public override void Die()
        {
            _fsm.ChangeState<EnemyState_Die>();
        }

        public override void Turn()
        {
            _fsm.EnemyTurn();
        }
    }
}