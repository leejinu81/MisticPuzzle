using ModestTree;
using System.Collections.Generic;
using Zenject;

namespace Lonely
{
    public interface IStateBinder
    {
        IStateBinder Enter<TStateEnter>() where TStateEnter : IStateEnter;

        IStateBinder Exit<TStateExit>() where TStateExit : IStateExit;

        IStateBinder Update<TStateUpdate>() where TStateUpdate : IStateUpdate;

        IStateBinder Turn<TStateTurn>() where TStateTurn : ITurnable;

        IStateBinder Shield<TTitanShield>() where TTitanShield : ITitanShield;

        TState Make<TState>() where TState : State;
    }

    public class StateBinder : IStateBinder
    {
        public IStateBinder Enter<TStateEnter>() where TStateEnter : IStateEnter
        {
            _enter = _container.Instantiate<TStateEnter>();
            return this;
        }

        public IStateBinder Exit<TStateExit>() where TStateExit : IStateExit
        {
            _exit = _container.Instantiate<TStateExit>();
            return this;
        }

        public IStateBinder Update<TStateUpdate>() where TStateUpdate : IStateUpdate
        {
            _update = _container.Instantiate<TStateUpdate>();
            return this;
        }

        public IStateBinder Turn<TStateTurn>() where TStateTurn : ITurnable
        {
            _turnable = _container.Instantiate<TStateTurn>();
            return this;
        }

        public IStateBinder Shield<TTitanShield>() where TTitanShield : ITitanShield
        {
            _titanShield = _container.Instantiate<TTitanShield>();
            return this;
        }

        public TState Make<TState>() where TState : State
        {
            return _container.Instantiate<TState>(GatherParams<TState>());
        }

        protected readonly DiContainer _container;
        private IStateEnter _enter = StateEnter.Null;
        private IStateExit _exit = StateExit.Null;
        private IStateUpdate _update = StateUpdate.Null;
        private ITurnable _turnable = Turnable.Null;
        private ITitanShield _titanShield = TitanShield.Null;
        
        public static StateBinder Bind(DiContainer container)
        {
            return new StateBinder(container);
        }

        protected StateBinder(DiContainer container)
        {
            _container = container;
        }

        private List<object> GatherParams<TState>() where TState : State
        {
            var paramList = new List<object> { _enter, _exit, _update };

            if (typeof(TState).DerivesFrom<ITurnable>())
                paramList.Add(_turnable);
            if (typeof(TState).DerivesFrom<ITitanShield>())
                paramList.Add(_titanShield);

            return paramList;
        }
    }
}