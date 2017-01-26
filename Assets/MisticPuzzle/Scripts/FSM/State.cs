using Zenject;

namespace Lonely
{
    public interface IStateEnter
    {
        void Enter();
    }

    public interface IStateExit
    {
        void Exit();
    }

    public interface IStateUpdate
    {
        void Update();
    }

    public abstract class State : IStateEnter, IStateExit, IStateUpdate
    {
        #region interface

        public virtual void Enter()
        {
            _enter.Enter();
        }

        public virtual void Exit()
        {
            _exit.Exit();
        }

        public virtual void Update()
        {
            _update.Update();
        }

        #endregion interface

        private readonly IStateEnter _enter;
        private readonly IStateExit _exit;
        private readonly IStateUpdate _update;

        public State(IStateEnter enter, IStateExit exit, IStateUpdate update)
        {
            _enter = enter;
            _exit = exit;
            _update = update;
        }

        public static State Null = new NullState();

        private class NullState : State
        {
            public NullState()
                : base(StateEnter.Null, StateExit.Null, StateUpdate.Null)
            { }
        }

        public class Factory : Factory<State>
        { }
    }

    public abstract class StateEnter : IStateEnter
    {
        void IStateEnter.Enter()
        {
        }

        public static readonly IStateEnter Null = new NullStateEnter();

        private class NullStateEnter : IStateEnter
        {
            void IStateEnter.Enter() { }
        }
    }

    public abstract class StateExit : IStateExit
    {
        void IStateExit.Exit()
        {
        }

        public static readonly IStateExit Null = new NullStateExit();

        private class NullStateExit : IStateExit
        {
            void IStateExit.Exit() { }
        }
    }

    public abstract class StateUpdate : IStateUpdate
    {
        void IStateUpdate.Update()
        {
        }

        public static readonly IStateUpdate Null = new NullStateUpdate();

        private class NullStateUpdate : IStateUpdate
        {
            void IStateUpdate.Update() { }
        }
    }
}