using Zenject;

namespace Lonely
{
    public class EnemyState_Die : GuardianState
    {
        public EnemyState_Die(IStateEnter enter, IStateExit exit, IStateUpdate update, ITurnable turnable)
            : base(enter, exit, update, turnable)
        {
        }

        public class CustomFactory : IFactory<GuardianState>
        {
            #region interface

            GuardianState IFactory<GuardianState>.Create()
            {
                var binder = GuardianStateBinder<EnemyState_Die>.For(_container);
                return binder.Enter<EnemyStateDie_Enter>().Make();
            }

            #endregion interface

            private readonly DiContainer _container;

            public CustomFactory(DiContainer container)
            {
                _container = container;
            }
        }
    }

    public class EnemyStateDie_Enter : IStateEnter
    {
        void IStateEnter.Enter()
        {
            _model.enableGameObject = false;
        }

        private readonly EnemyModel _model;

        public EnemyStateDie_Enter(EnemyModel model)
        {
            _model = model;
        }
    }
}