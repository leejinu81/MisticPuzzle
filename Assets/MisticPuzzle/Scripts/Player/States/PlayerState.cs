namespace Lonely
{
    public class PlayerState : State
    {
        public void Turn()
        {
            _turnable.Turn();
        }

        private readonly ITurnable _turnable;

        public PlayerState(IStateEnter enter, IStateExit exit, IStateUpdate update, ITurnable turnable)
            : base(enter, exit, update)
        {
            _turnable = turnable;
        }
    }
}