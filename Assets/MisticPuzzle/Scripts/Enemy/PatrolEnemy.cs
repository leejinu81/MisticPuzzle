using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Lonely
{
    public class PatrolEnemy : MonoBehaviour
    {
        private PatrolEnemyFSM _fsm;

        [Inject]
        private void Inject(PatrolEnemyFSM fsm)
        {
            _fsm = fsm;
        }
    }

}