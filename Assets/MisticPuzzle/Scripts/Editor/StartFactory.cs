using Extension;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Lonely.Editor
{
    /// <summary>
    /// Editor에서는 GameObject 생성으로 Inject하는 것이 안됨. 그래서 Inject없이 강제 생성
    /// </summary>
    public class StartFactory : IFactory<int, int, GameObject>
    {
        #region Explicit Interface

        GameObject IFactory<int, int, GameObject>.Create(int row, int column)
        {
            var startPrefab = AssetDatabase.LoadAssetAtPath<Object>(START_PREFAB_PATH);
            Debug.Assert(startPrefab.IsValid());

            var startGO = PrefabUtility.InstantiatePrefab(startPrefab) as GameObject;
            Debug.Assert(startGO.IsValid());

            var gridCalc = new GridPositionCalculator(row, column);
            startGO.transform.localPosition = gridCalc.leftBottom;

            return startGO;
        }

        #endregion Explicit Interface

        private const string START_PREFAB_PATH = "Assets/MisticPuzzle/Prefabs/Start.prefab";
    }
}