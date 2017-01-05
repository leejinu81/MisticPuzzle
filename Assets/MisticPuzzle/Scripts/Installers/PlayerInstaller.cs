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
            Container.Bind<PlayerModel>().To<PlayerModel>().AsSingle();

            InstallPlayerState();

            InstallSettings();
        }

        private void InstallPlayerState()
        {
            Container.BindAllInterfacesAndSelf<PlayerFSM>().To<PlayerFSM>().AsSingle();
            // Circular dependency 때문에 Factory로
            Container.BindFactory<PlayerState_Idle, PlayerState_Idle.Factory>();
            Container.BindFactory<PlayerState_Move, PlayerState_Move.Factory>();
            Container.BindFactory<PlayerState_Block, PlayerState_Block.Factory>();
            Container.BindFactory<PlayerState_Kill, PlayerState_Kill.Factory>();
            Container.BindFactory<PlayerState_Die, PlayerState_Die.Factory>();
        }

        private void InstallSettings()
        {
            Container.BindInstance(_settings.playerCollider2D);
            Container.BindInstance(_settings.playerTransform);
            Container.BindInstance(_settings.playerGameObject);
            Container.BindInstance(_settings.moveTime);
            Container.BindInstance(_settings.blockingLayer);
        }

        [Serializable]
        public class Settings
        {
            public Collider2D playerCollider2D;
            public Transform playerTransform;
            public GameObject playerGameObject;
            public float moveTime;
            public LayerMask blockingLayer;
        }
    }
}