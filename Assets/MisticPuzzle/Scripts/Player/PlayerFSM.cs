using System.Collections.Generic;
using Zenject;

namespace Lonely
{
    public class PlayerFSM : FSM<State>
    {
        public PlayerFSM(List<State.Factory> stateFactoryList)
            : base(stateFactoryList.ConvertAll(x => x as IFactory<State>), State.Null)
        {
        }

        public void PlayerTurn()
        {
        }
    }
}