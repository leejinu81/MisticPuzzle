using Zenject;

namespace Lonely
{
    public class EnemyState_Kill : IState
    {
        #region Explicit Interface

        void IState.Enter()
        {
        }

        void IState.Exit()
        {
        }

        #endregion Explicit Interface

        public EnemyState_Kill()
        {
        }

        public class Factory : Factory<EnemyState_Kill>
        { }
    }
}