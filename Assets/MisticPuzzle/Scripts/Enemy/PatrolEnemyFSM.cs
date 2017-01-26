using System.Collections.Generic;
using Zenject;

namespace Lonely
{
    public class PatrolEnemyFSM : FSM<EnemyState>
    {
        public bool hasTitanSheild { get { return _curState.hasTitanSheild; } }

        public PatrolEnemyFSM(List<EnemyState.Factory> stateFactoryList)
            : base(stateFactoryList.ConvertAll(x => x as IFactory<EnemyState>), EnemyState.Null)        
        {
        }

        public void EnemyTurn()
        {
            _curState.Turn();
        }
    }
}