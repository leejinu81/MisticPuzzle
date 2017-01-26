using Extension;
using System.Collections.Generic;
using UnityEngine;

namespace Lonely
{
    public class GameManager
    {
        private readonly Player _player;
        private readonly List<EnemyFacade> _enemies;

        public GameManager(Player player, List<EnemyFacade> enemies)
        {
            _player = player;
            _enemies = enemies;

            var GO = GameObject.Find("Start");
            if (GO.IsValid())
                _player.transform.position = GO.transform.position;
        }

        public void PlayerTurn()
        {
            _player.PlayerTurn();
        }

        public void EnemyTurn()
        {
            foreach (var e in _enemies)
            {
                e.Turn();
            }
        }
    }
}