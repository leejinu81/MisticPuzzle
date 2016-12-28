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
        }
    }
}