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
            Container.BindFactory<State, State.Factory>().FromFactory<PlayerState_Idle.CustomFactory>();
            Container.BindFactory<State, State.Factory>().FromFactory<PlayerState_Move.CustomFactory>();
            Container.BindFactory<State, State.Factory>().FromFactory<PlayerState_Block.CustomFactory>();
            Container.BindFactory<State, State.Factory>().FromFactory<PlayerState_Kill.CustomFactory>();
            Container.BindFactory<State, State.Factory>().FromFactory<PlayerState_Die.CustomFactory>();
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