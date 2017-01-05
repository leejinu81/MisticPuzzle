using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Lonely
{
    public class GameManager : IInitializable
    {
        #region Explicit Interface

        void IInitializable.Initialize()
        {
            // FIXME
            var GO = GameObject.Find("Start");
            _player.transform.position = GO.transform.position;
        }

        #endregion Explicit Interface

        private readonly Player _player;
        private readonly List<Enemy> _enemys;

        public GameManager(Player player, List<Enemy> enemys)
        {
            _player = player;
            _enemys = enemys;
        }

        public void EnemyTurn()
        {
            foreach(var e in _enemys)
            {
                e.EnemyTurn();
            }
        }
    }    
}