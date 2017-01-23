using System.Collections.Generic;
using Zenject;

namespace Lonely
{
    public class StateBinder<TState>
            where TState : State
    {
        public StateBinder<TState> Enter<TStateEnter>()
            where TStateEnter : IStateEnter
        {
            _enter = _container.Instantiate<TStateEnter>();
            return this;
        }

        public StateBinder<TState> Exit<TStateExit>()
            where TStateExit : IStateExit
        {
            _exit = _container.Instantiate<TStateExit>();
            return this;
        }

        public StateBinder<TState> Update<TStateUpdate>()
            where TStateUpdate : IStateUpdate
        {
            _update = _container.Instantiate<TStateUpdate>();
            return this;
        }

        public TState Make()
        {
            return _container.Instantiate<TState>(GatherParams());
        }

        protected readonly DiContainer _container;
        private IStateEnter _enter = StateEnter.Null;
        private IStateExit _exit = StateExit.Null;
        private IStateUpdate _update = StateUpdate.Null;

        // 좋은 이름 없을까..
        public static StateBinder<TState> For(DiContainer container)
        {
            return new StateBinder<TState>(container);
        }

        protected StateBinder(DiContainer container)
        {
            _container = container;
        }

        protected virtual List<object> GatherParams()
        {
            return new List<object> { _enter, _exit, _update };
        }
    }
}