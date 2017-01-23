using Zenject;

namespace Lonely
{
    public class EnemyState_Kill : GuardianState
    {
        public EnemyState_Kill(IStateEnter enter, IStateExit exit, IStateUpdate update, ITurnable turnable)
            : base(enter, exit, update, turnable)
        {
        }

        public class CustomFactory : IFactory<GuardianState>
        {
            #region interface

            GuardianState IFactory<GuardianState>.Create()
            {
                var binder = GuardianStateBinder<EnemyState_Kill>.For(_container);
                return binder.Enter<EnemyStateKill_Enter>().Make();
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