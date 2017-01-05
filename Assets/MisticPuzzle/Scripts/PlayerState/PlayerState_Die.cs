using Zenject;

namespace Lonely
{
    public class PlayerState_Die : IState
    {
        #region Explicit Interface

        void IState.Enter()
        {
            _model.enableGameObject = false;
            _dieCommand.Execute();
        }

        void IState.Exit()
        {
        }

        #endregion Explicit Interface

        private readonly PlayerModel _model;
        private readonly GameCommands.Die _dieCommand;

        public PlayerState_Die(PlayerModel model, GameCommands.Die dieCommand)
        {
            _model = model;
            _dieCommand = dieCommand;
        }

        public class Factory : Factory<PlayerState_Die>
        { }
    }
}