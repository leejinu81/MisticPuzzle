using System.Collections.Generic;
using Zenject;

namespace Lonely
{
    public class GuardianFSM : FSM<GuardianState>
    {
        public bool isTitanShield { get { return _curState is ITitanShield; } }

        public GuardianFSM(List<GuardianState.Factory> stateFactoryList)
            : base(stateFactoryList.ConvertAll(x => x as IFactory<GuardianState>))
        {
        }

        public void EnemyTurn()
        {
            _curState.Turn();
        }
    }
}