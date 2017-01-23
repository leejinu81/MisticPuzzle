using UnityEngine;
using Zenject;

namespace Lonely
{
    public class Enemy : MonoBehaviour
    {
        private GuardianFSM _fsm;

        public bool isTitanShield { get { return _fsm.isTitanShield; } }

        private void Start()
        {
            _fsm.ChangeState<EnemyState_Idle>();
        }

        [Inject]
        private void Inject(GuardianFSM fsm)
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