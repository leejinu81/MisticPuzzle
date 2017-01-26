﻿using ModestTree;
using System;
using UnityEngine;
using Zenject;

namespace Lonely
{
    public class PatrolEnemyInstaller : MonoInstaller<PatrolEnemyInstaller>
    {
        [SerializeField]
        private Settings _settings = null;

        public override void InstallBindings()
        {
            Container.Bind<EnemyModel>().To<EnemyModel>().AsSingle();

            Container.BindInstance(_settings.enemyCollider2D);
            Container.BindInstance(_settings.enemyTransform);
            Container.BindInstance(_settings.enemyGameObject);
            Container.BindInstance(_settings.enemySprite);

            Container.BindInstance(_settings.blockingLayer);
            Container.BindInstance(_settings.moveTime);
            Container.BindInstance(_settings.direction);

            InstallStates();
            InitExecutionOrder();
        }

        private void InstallStates()
        {
            Container.Bind<IInitializable>().To<PatrolEnemy>().AsSingle();
            Container.Bind<Enemy>().To<PatrolEnemy>().AsSingle();
                        
            Container.BindAllInterfacesAndSelf<PatrolEnemyFSM>().To<PatrolEnemyFSM>().AsSingle();
            Container.BindFactory<EnemyState, EnemyState.Factory>().FromFactory<EnemyState_Patrol.CustomFactory>();
            Container.BindFactory<EnemyState, EnemyState.Factory>().FromFactory<EnemyState_Kill.CustomFactory>();
            Container.BindFactory<EnemyState, EnemyState.Factory>().FromFactory<EnemyState_Die.CustomFactory>();
        }

        private void InitExecutionOrder()
        {            
            Container.BindExecutionOrder<PatrolEnemy>(-10);
            Container.BindExecutionOrder<PatrolEnemyFSM>(-20);
        }
    }

    [Serializable]
    public class Settings
    {
        public Collider2D enemyCollider2D;
        public Transform enemyTransform;
        public GameObject enemyGameObject;
        public SpriteRenderer enemySprite;

        public LayerMask blockingLayer;
        public float moveTime;
        public eDirection direction;
    }
}