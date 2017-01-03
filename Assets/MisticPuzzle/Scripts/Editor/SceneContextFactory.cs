using Extension;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Zenject;

namespace Lonely.Editor
{
    /// <summary>
    /// Editor에서는 GameObject 생성으로 Inject하는 것이 안됨. 그래서 Inject없이 강제 생성
    /// </summary>
    public class SceneContextFactory : IFactory<SceneContext>
    {
        #region Explicit Interface

        SceneContext IFactory<SceneContext>.Create()
        {
            var scPrefab = AssetDatabase.LoadAssetAtPath<Object>(SCENE_CONTEXT_PREFAB_PATH);
            Debug.Assert(scPrefab.IsValid());

            var scGO = PrefabUtility.InstantiatePrefab(scPrefab) as GameObject;
            Debug.Assert(scGO.IsValid());

            var context = scGO.GetComponent<SceneContext>();
            Debug.Assert(context.IsValid());

            //Selection.activeGameObject = root.gameObject;

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

            return context;
        }

        #endregion Explicit Interface

        private const string SCENE_CONTEXT_PREFAB_PATH = "Assets/MisticPuzzle/Prefabs/SceneContext.prefab";
    }
}