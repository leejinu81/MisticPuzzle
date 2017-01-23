using System.Collections.Generic;
using Zenject;

namespace Lonely
{
    public class GuardianStateBinder<TState> : StateBinder<TState>
        where TState : GuardianState
    {
        private ITurnable _turnable = Turnable.Null;

        public static new GuardianStateBinder<TState> For(DiContainer container)
        {
            return new GuardianStateBinder<TState>(container);
        }

        protected GuardianStateBinder(DiContainer container)
            : base(container)
        {
        }

        public GuardianStateBinder<TState> Turn<TStateTurn>()
            where TStateTurn : ITurnable
        {
            _turnable = _container.Instantiate<TStateTurn>();
            return this;
        }

        protected override List<object> GatherParams()
        {
            var paramList = base.GatherParams();
            paramList.Add(_turnable);
            return paramList;
        }
    }
}