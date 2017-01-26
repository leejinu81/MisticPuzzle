using UnityEngine;
using Zenject;

namespace Lonely
{
    public class EnemyFacade : MonoBehaviour
    {
        private Enemy _enemy;

        [Inject]
        private void Inject(Enemy enemy)
        {
            _enemy = enemy;
        }

        public bool hasTitanSheild { get { return _enemy.hasTitanSheild; } }

        public void Die() { _enemy.Die(); }

        public void Turn() { _enemy.Turn(); }
    }
}