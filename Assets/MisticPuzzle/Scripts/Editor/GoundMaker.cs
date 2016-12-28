using Extension;
using System;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Lonely.Editor
{
    public class GoundMaker : IInitializable, IGuiRenderable, IDisposable
    {
        #region Explicit Interface

        void IInitializable.Initialize()
        {
            // Load EditorPrefs
            _info.wallPrefab = EditorUtility.LoadAssetInPrefs<UnityEngine.Object>(WALL_LABEL, this);
            _info.floorPrefab = EditorUtility.LoadAssetInPrefs<UnityEngine.Object>(FLOOR_LABEL, this);
        }

        void IGuiRenderable.GuiRender()
        {
            _info.row = EditorGUILayout.IntSlider("Row : ", _info.row, 1, ROW_MAX);
            _info.column = EditorGUILayout.IntSlider("Column : ", _info.column, 1, COLUMN_MAX);

            GUIRenderButtonCreate();

            if (EditorUtility.FoldOut("Prefabs", this))
            {
                using (EditorUtility.Contents())
                {
                    _info.wallPrefab = EditorGUILayout.ObjectField(WALL_LABEL, _info.wallPrefab, typeof(UnityEngine.Object), false);
                    _info.floorPrefab = EditorGUILayout.ObjectField(FLOOR_LABEL, _info.floorPrefab, typeof(UnityEngine.Object), false);
                }
            }
        }

        void IDisposable.Dispose()
        {
            // Save EditorPrefs
            EditorUtility.SavePrefs(_info.wallPrefab, WALL_LABEL, this);
            EditorUtility.SavePrefs(_info.floorPrefab, FLOOR_LABEL, this);
        }

        #endregion Explicit Interface

        private const int ROW_MAX = 20;
        private const int COLUMN_MAX = 20;
        private const string WALL_LABEL = "Wall Prefab";
        private const string FLOOR_LABEL = "Floor Prefab";
        private const int UNIT = 1;

        private readonly GroundInfo _info;

        public GoundMaker(GroundInfo info)
        {
            _info = info;
        }

        private void GUIRenderButtonCreate()
        {
            GUI.enabled = _info.floorPrefab.IsValid() && _info.wallPrefab.IsValid();
            if (GUILayout.Button("Create"))
            {
                CreateFloors();
                CreateWalls();
            }
            GUI.enabled = true;
        }

        private void CreateFloors()
        {
            var halfUnit = UNIT * 0.5f;
            var left = (-halfUnit * _info.row) + halfUnit;
            var bottom = (-halfUnit * _info.column) + halfUnit;

            GameObject floorGO = new GameObject("Floors");
            for (int i = 0; i < _info.row; i++)
            {
                for (int j = 0; j < _info.column; j++)
                {
                    var floorObj = PrefabUtility.InstantiatePrefab(_info.floorPrefab) as GameObject;
                    floorObj.transform.localPosition = new Vector3(left + (i * UNIT), bottom + (j * UNIT));
                    floorObj.transform.parent = floorGO.transform;
                }
            }
        }

        private void CreateWalls()
        {
            var halfUnit = UNIT * 0.5f;
            var wallRow = _info.row + 2;
            var wallColumn = _info.column + 2;
            var left = (-halfUnit * wallRow) + halfUnit;
            var bottom = (-halfUnit * wallColumn) + halfUnit;

            GameObject wallGO = new GameObject("Walls");
            for (int i = 0; i < wallRow; i++)
            {
                if (Equals(i, 0) || Equals(i, wallRow - 1))
                {
                    for (int j = 0; j < wallColumn; j++)
                    {
                        var wallObj = PrefabUtility.InstantiatePrefab(_info.wallPrefab) as GameObject;
                        wallObj.transform.localPosition = new Vector3(left + (i * UNIT), bottom + (j * UNIT));
                        wallObj.transform.parent = wallGO.transform;
                    }
                }
                else
                {
                    var firstWallObj = PrefabUtility.InstantiatePrefab(_info.wallPrefab) as GameObject;
                    firstWallObj.transform.localPosition = new Vector3(left + (i * UNIT), bottom + (0 * UNIT));
                    firstWallObj.transform.parent = wallGO.transform;

                    var lastWallObj = PrefabUtility.InstantiatePrefab(_info.wallPrefab) as GameObject;
                    lastWallObj.transform.localPosition = new Vector3(left + (i * UNIT), bottom + ((wallColumn - 1) * UNIT));
                    lastWallObj.transform.parent = wallGO.transform;
                }
            }
        }

        [Serializable]
        public class GroundInfo
        {
            public int row;
            public int column;

            public UnityEngine.Object wallPrefab;
            public UnityEngine.Object floorPrefab;
        }
    }
}