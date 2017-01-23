using Zenject;

namespace Lonely
{
    public class GuardianState : State, ITurnable
    {
        public void Turn()
        {
            _turnable.Turn();
        }

        private readonly ITurnable _turnable;

        public GuardianState(IStateEnter enter, IStateExit exit, IStateUpdate update, ITurnable turnable)
            : base(enter, exit, update)
        {
            _turnable = turnable;
        }

        public new class Factory : Factory<GuardianState>
        { }
    }
}