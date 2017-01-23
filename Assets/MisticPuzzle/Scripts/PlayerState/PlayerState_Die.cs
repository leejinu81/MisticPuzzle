using Zenject;

namespace Lonely
{
    public class PlayerState_Die : State
    {
        public PlayerState_Die(IStateEnter enter, IStateExit exit, IStateUpdate update)
            : base(enter, exit, update)
        {
        }        

        public class CustomFactory : IFactory<State>
        {
            #region interface

            State IFactory<State>.Create()
            {
                var binder = StateBinder<PlayerState_Die>.For(_container);
                return binder.Enter<PlayerStateDie_Enter>().Make();
            }

            #endregion interface

            private readonly DiContainer _container;

            public CustomFactory(DiContainer container)
            {
                _container = container;
            }
        }
    }    

    public class PlayerStateDie_Enter : IStateEnter
    {
        void IStateEnter.Enter()
        {
            _model.enableGameObject = false;
            _dieCommand.Execute();
        }

        private readonly PlayerModel _model;
        private readonly GameCommands.Die _dieCommand;

        public PlayerStateDie_Enter(PlayerModel model, GameCommands.Die dieCommand)
        {
            _model = model;
            _dieCommand = dieCommand;
        }
    }
}