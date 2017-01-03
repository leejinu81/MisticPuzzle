using System;
using UnityEngine;
using Zenject;

namespace Lonely
{
    public class PlayerInstaller : MonoInstaller<PlayerInstaller>
    {
        [SerializeField]
        private Settings _settings = null;

        public override void InstallBindings()
        {
            InstallPlayerState();

            InstallSettings();
        }        

        private void InstallPlayerState()
        {
            Container.BindAllInterfaces<PlayerFSM>().To<PlayerFSM>().AsSingle();
            // Circular dependency 때문에 Factory로        
            Container.BindFactory<PlayerState_Idle, PlayerState_Idle.Factory>();
            Container.BindFactory<PlayerState_Move, PlayerState_Move.Factory>();
        }

        private void InstallSettings()
        {
            Container.BindInstance(_settings.playerRigidbody2D);
            Container.BindInstance(_settings.playerCollider2D);
            Container.BindInstance(_settings.moveTime).WhenInjectedInto<PlayerState_Move>();
            Container.BindInstance(_settings.blockingLayer).WhenInjectedInto<PlayerState_Move>();
        }

        [Serializable]
        public class Settings
        {
            public Rigidbody2D playerRigidbody2D;
            public Collider2D playerCollider2D;
            public float moveTime;
            public LayerMask blockingLayer;                
        }
    }
}