using Zenject;

namespace Lonely
{
    public class EnemyState_Die : EnemyState
    {
        public EnemyState_Die(IStateEnter enter, IStateExit exit, IStateUpdate update,
                              ITurnable turnable, ITitanShield titanShield)
            : base(enter, exit, update, turnable, titanShield)
        {
        }

        public class CustomFactory : IFactory<EnemyState>
        {
            #region interface

            EnemyState IFactory<EnemyState>.Create()
            {
                return StateBinder.Bind(_container)
                                  .Enter<EnemyStateDie_Enter>()
                                  .Make<EnemyState_Die>();
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