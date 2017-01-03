using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Lonely.Editor
{
    public class GroundMakerWindow : ZenjectEditorWindow
    {
        private GoundMaker.GroundInfo _info = new GoundMaker.GroundInfo();

        [MenuItem("Window/GroundMakerWindow")]
        public static GroundMakerWindow GetOrCreateWindow()
        {
            var window = EditorWindow.GetWindow<GroundMakerWindow>();
            window.titleContent = new GUIContent("GroundMakerWindow");
            return window;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(_info);
            Container.BindAllInterfaces<GoundMaker>().To<GoundMaker>().AsSingle();

            Container.Bind<IFactory<SceneContext>>().To<SceneContextFactory>().AsSingle();
            Container.Bind<IFactory<SceneContext, Object, Player>>().To<PlayerFactory>().AsSingle();
            Container.Bind<IFactory<int, int, GameObject>>().WithId("start").To<StartFactory>().AsSingle();
            Container.Bind<IFactory<int, int, GameObject>>().WithId("exit").To<ExitFactory>().AsSingle();
            Container.Bind<IFactory<Canvas>>().To<UIFactory>().AsSingle();
            Container.Bind<IFactory<int, int, Object, IEnumerable<GameObject>>>().WithId("floor").To<FloorFactory>().AsSingle();
            Container.Bind<IFactory<int, int, Object, IEnumerable<GameObject>>>().WithId("wall").To<WallFactory>().AsSingle();
        }
    }
}