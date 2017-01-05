using System;
using UnityEngine;
using Zenject;

namespace Lonely
{
    public class EnemyInstaller : MonoInstaller<EnemyInstaller>
    {
        [SerializeField]
        private Settings _settings = null;

        public override void InstallBindings()
        {
            Container.Bind<EnemyModel>().To<EnemyModel>().AsSingle();
            Container.Bind<EnemyEye>().To<EnemyEye>().AsSingle();

            Container.BindAllInterfacesAndSelf<EnemyFSM>().To<EnemyFSM>().AsSingle();
            Container.BindFactory<EnemyState_Idle, EnemyState_Idle.Factory>();
            Container.BindFactory<EnemyState_MoveToTarget, EnemyState_MoveToTarget.Factory>();
            Container.BindFactory<EnemyState_Return, EnemyState_Return.Factory>();
            Container.BindFactory<EnemyState_Kill, EnemyState_Kill.Factory>();
            Container.BindFactory<EnemyState_Die, EnemyState_Die.Factory>();

            Container.BindInstance(_settings.enemyCollider2D);
            Container.BindInstance(_settings.enemyTransform);
            Container.BindInstance(_settings.enemyGameObject);
            Container.BindInstance(_settings.enemySprite);            

            Container.BindInstance(_settings.blockingLayer);
            Container.BindInstance(_settings.direction);
        }

        [Serializable]
        public class Settings
        {
            public Collider2D enemyCollider2D;
            public Transform enemyTransform;
            public GameObject enemyGameObject;
            public SpriteRenderer enemySprite;            

            public LayerMask blockingLayer;
            public eDirection direction;
        }
    }
}