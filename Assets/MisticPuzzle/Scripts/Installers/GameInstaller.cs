using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Lonely
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        [SerializeField]
        private Settings _settings = null;

        public override void InstallBindings()
        {
            Container.BindAllInterfacesAndSelf<MisticPuzzleInput>().To<MisticPuzzleInput>().AsSingle();

            Container.BindAllInterfacesAndSelf<GameManager>().To<GameManager>().AsSingle();

            Container.BindFactory<List<IState>, FSM, FSM.Factory>();

            Container.BindAllInterfacesAndSelf<UIManager>().To<UIManager>().AsSingle();

            Container.BindCommand<GameCommands.EnemyTurn>().To<GameManager>(x => x.EnemyTurn).AsSingle();
            Container.BindCommand<GameCommands.PlayerTurn>().To<GameManager>(x => x.PlayerTurn).AsSingle();
            Container.BindCommand<GameCommands.Escape>().To<UIManager>(x => x.Escape).AsSingle();
            Container.BindCommand<GameCommands.Die>().To<UIManager>(x => x.Die).AsSingle();

            Container.BindFactory<GameObject, TargetPositionFactory>().FromPrefab(_settings.targetPointPrefab);
        }

        [Serializable]
        public class Settings
        {
            public GameObject targetPointPrefab;
        }
    }
}