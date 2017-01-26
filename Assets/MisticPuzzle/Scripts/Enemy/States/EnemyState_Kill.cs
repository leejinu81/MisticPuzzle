using Zenject;

namespace Lonely
{
    public class EnemyState_Kill : EnemyState
    {
        public EnemyState_Kill(IStateEnter enter, IStateExit exit, IStateUpdate update,
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
                                  .Enter<EnemyStateKill_Enter>()
                                  .Make<EnemyState_Kill>();
            }

            #endregion interface

            private readonly DiContainer _container;

            public CustomFactory(DiContainer container)
            {
                _container = container;
            }
        }
    }

    public class EnemyStateKill_Enter : IStateEnter
    {
        void IStateEnter.Enter()
        {
            _model.DOMove(_model.targetPos, _moveTime);
        }

        private readonly EnemyModel _model;
        private readonly float _moveTime;

        public EnemyStateKill_Enter(EnemyModel model, float moveTime)
        {
            _model = model;
            _moveTime = moveTime;
        }
    }
}