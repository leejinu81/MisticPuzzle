namespace Lonely
{
    public interface IState
    {
        void Enter();

        void Exit();
    }

    public static class State
    {
        public static readonly IState Null = new NullState();

        private class NullState : IState
        {
            void IState.Enter() { }

            void IState.Exit() { }
        }
    }
}