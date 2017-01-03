using Extension;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Lonely.Editor
{
    /// <summary>
    /// Editor에서는 GameObject 생성으로 Inject하는 것이 안됨. 그래서 Inject없이 강제 생성
    /// </summary>
    public class UIFactory : IFactory<Canvas>
    {
        #region Explicit Interface

        Canvas IFactory<Canvas>.Create()
        {
            var uiPrefab = AssetDatabase.LoadAssetAtPath<Object>(UI_PREFAB_PATH);
            Debug.Assert(uiPrefab.IsValid());

            var uiGO = PrefabUtility.InstantiatePrefab(uiPrefab) as GameObject;
            Debug.Assert(uiGO.IsValid());

            var canvas = uiGO.GetComponent<Canvas>();
            Debug.Assert(canvas.IsValid());

            return canvas;
        }

        #endregion Explicit Interface

        private const string UI_PREFAB_PATH = "Assets/MisticPuzzle/Prefabs/Canvas.prefab";
    }
}