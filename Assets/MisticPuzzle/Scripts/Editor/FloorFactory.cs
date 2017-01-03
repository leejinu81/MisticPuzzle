using Extension;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Lonely.Editor
{
    /// <summary>
    /// Editor에서는 GameObject 생성으로 Inject하는 것이 안됨. 그래서 Inject없이 강제 생성
    /// </summary>
    public class FloorFactory : IFactory<int, int, Object, IEnumerable<GameObject>>
    {
        #region Explicit Interface

        IEnumerable<GameObject> IFactory<int, int, Object, IEnumerable<GameObject>>.Create(int row, int column, Object prefab)
        {
            var parentGO = new GameObject("Floors");
            var gridCalc = new GridPositionCalculator(row, column);
            var floorList = new List<GameObject>();

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    var floorGO = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                    Debug.Assert(floorGO.IsValid());

                    floorGO.transform.localPosition = gridCalc.Calc(i, j);
                    floorGO.transform.parent = parentGO.transform;
                    floorList.Add(floorGO);
                }
            }

            return floorList;
        }

        #endregion Explicit Interface
    }
}