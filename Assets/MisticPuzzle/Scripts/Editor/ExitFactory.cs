using Extension;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Lonely.Editor
{
    /// <summary>
    /// Editor에서는 GameObject 생성으로 Inject하는 것이 안됨. 그래서 Inject없이 강제 생성
    /// </summary>
    public class ExitFactory : IFactory<int, int, GameObject>
    {
        #region Explicit Interface

        GameObject IFactory<int, int, GameObject>.Create(int row, int column)
        {
            var exitPrefab = AssetDatabase.LoadAssetAtPath<Object>(EXIT_PREFAB_PATH);
            Debug.Assert(exitPrefab.IsValid());

            var exitGO = PrefabUtility.InstantiatePrefab(exitPrefab) as GameObject;
            Debug.Assert(exitGO.IsValid());

            var gridCalc = new GridPositionCalculator(row, column);
            exitGO.transform.localPosition = gridCalc.rightTop;

            return exitGO;
        }

        #endregion Explicit Interface

        private const string EXIT_PREFAB_PATH = "Assets/MisticPuzzle/Prefabs/Exit.prefab";
    }
}