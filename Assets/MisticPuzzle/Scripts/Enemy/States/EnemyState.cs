using Zenject;

namespace Lonely
{
    public class EnemyState : State, ITurnable, ITitanShield
    {
        public bool hasTitanSheild { get { return _titanShield.hasTitanSheild; } }

        private readonly ITurnable _turnable;
        private readonly ITitanShield _titanShield;

        public EnemyState(IStateEnter enter, IStateExit exit, IStateUpdate update,
                             ITurnable turnable, ITitanShield titanShield)
            : base(enter, exit, update)
        {
            _turnable = turnable;
            _titanShield = titanShield;
        }

        public void Turn() { _turnable.Turn(); }

        public static new readonly EnemyState Null = new NullState();

        private class NullState : EnemyState
        {
            public NullState()
                : base(StateEnter.Null, StateExit.Null, StateUpdate.Null, Turnable.Null, TitanShield.Null)
            { }
        }

        public new class Factory : Factory<EnemyState>
        { }
    }

    public interface ITurnable
    {
        void Turn();
    }

    public abstract class Turnable : ITurnable
    {
        void ITurnable.Turn()
        { }

        public static readonly ITurnable Null = new NullTurnable();

        private class NullTurnable : ITurnable
        {
            void ITurnable.Turn() { }
        }
    }

    public interface ITitanShield
    {
        bool hasTitanSheild { get; }
    }

    public class TitanShield : ITitanShield
    {
        bool ITitanShield.hasTitanSheild { get { return true; } }

        public static readonly ITitanShield Null = new NullTitanShield();

        private class NullTitanShield : ITitanShield
        {
            bool ITitanShield.hasTitanSheild { get { return false; } }
        }
    }
}