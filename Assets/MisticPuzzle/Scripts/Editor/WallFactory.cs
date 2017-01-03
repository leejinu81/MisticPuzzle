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
    public class WallFactory : IFactory<int, int, Object, IEnumerable<GameObject>>
    {
        #region Explicit Interface

        IEnumerable<GameObject> IFactory<int, int, Object, IEnumerable<GameObject>>.Create(int row, int column, Object prefab)
        {
            var parentGO = new GameObject("Walls");

            var wallRow = row + 2;
            var wallColumn = column + 2;
            var gridCalc = new GridPositionCalculator(wallRow, wallColumn);
            var wallList = new List<GameObject>();

            for (int i = 0; i < wallRow; i++)
            {
                if (IsFirstOrLastRow(i, gridCalc.lastRow))
                {
                    for (int j = 0; j < wallColumn; j++)
                    {
                        var wallGO = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                        Debug.Assert(wallGO.IsValid());

                        wallGO.transform.localPosition = gridCalc.Calc(i, j);
                        wallGO.transform.parent = parentGO.transform;
                        wallList.Add(wallGO);
                    }
                }
                else
                {
                    var firstColumnWallGO = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                    Debug.Assert(firstColumnWallGO.IsValid());

                    firstColumnWallGO.transform.localPosition = gridCalc.Calc(i, 0);
                    firstColumnWallGO.transform.parent = parentGO.transform;
                    wallList.Add(firstColumnWallGO);

                    var lastColumnWallGO = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                    Debug.Assert(lastColumnWallGO.IsValid());

                    lastColumnWallGO.transform.localPosition = gridCalc.Calc(i, gridCalc.lastColumn);
                    lastColumnWallGO.transform.parent = parentGO.transform;
                    wallList.Add(lastColumnWallGO);
                }
            }

            return wallList;
        }

        #endregion Explicit Interface

        private bool IsFirstOrLastRow(int i, int lastRow)
        {
            return Equals(i, 0) || Equals(i, lastRow);
        }
    }
}