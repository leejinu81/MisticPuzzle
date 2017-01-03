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

        public GameManager(Player player)
        {
            _player = player;
        }
    }
}