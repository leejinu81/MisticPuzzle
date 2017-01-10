using Zenject;

namespace Lonely
{
    public class EnemyState_Kill : IState
    {
        #region Explicit Interface

        void IState.Enter()
        {
            _model.DOMove(_model.targetPos, _moveTime);
        }

        void IState.Exit()
        {
        }

        #endregion Explicit Interface

        private readonly EnemyModel _model;
        private readonly float _moveTime;

        public EnemyState_Kill(EnemyModel model, float moveTime)
        {
            _model = model;
            _moveTime = moveTime;
        }

        public class Factory : Factory<EnemyState_Kill>
        { }
    }
}