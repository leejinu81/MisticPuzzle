using Extension;
using UnityEngine;
using Zenject;

namespace Lonely
{
    public class Enemy : MonoBehaviour
    {
        private EnemyFSM _fsm;

        // EnemyModel로 가야하나?
        public bool isTitanShield
        {
            get
            {
                var ts = _fsm.curState as ITitanShield;
                if (ts.IsValid())
                    return true;
                else
                    return false;
            }
        }

        private void Start()
        {
            _fsm.ChangeState<EnemyState_Idle>();
        }

        [Inject]
        private void Inject(EnemyFSM fsm)
        {
            _fsm = fsm;
        }

        public void EnemyTurn()
        {
            _fsm.EnemyTurn();
        }

        public void Die()
        {
            _fsm.ChangeState<EnemyState_Die>();
        }
    }

    public class TargetPositionFactory : Factory<GameObject>
    {
    }
}