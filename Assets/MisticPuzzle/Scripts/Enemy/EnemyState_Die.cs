using Zenject;

namespace Lonely
{
    public class EnemyState_Die : IState
    {
        #region Explicit Interface

        void IState.Enter()
        {
            _model.enableGameObject = false;
        }

        void IState.Exit()
        {
        }

        #endregion Explicit Interface

        private readonly EnemyModel _model;

        public EnemyState_Die(EnemyModel model)
        {
            _model = model;
        }

        public class Factory : Factory<EnemyState_Die>
        { }
    }
}