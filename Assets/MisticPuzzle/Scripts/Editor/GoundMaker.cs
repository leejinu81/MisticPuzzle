using Extension;
using System;
using System.Collections.Generic;
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
            _info.wallPrefab = EditorUtility.LoadAssetInPrefs<UnityEngine.Object>(WALL_LABEL, this);
            _info.floorPrefab = EditorUtility.LoadAssetInPrefs<UnityEngine.Object>(FLOOR_LABEL, this);
            _info.playerPrefab = EditorUtility.LoadAssetInPrefs<UnityEngine.Object>(PLAYER_LABEL, this);
        }

        void IGuiRenderable.GuiRender()
        {
            GUIRender_RowColumn();
            GUIRender_ButtonCreate();
            GUIRender_Prefabs();
        }

        void IDisposable.Dispose()
        {
            EditorUtility.SavePrefs(_info.wallPrefab, WALL_LABEL, this);
            EditorUtility.SavePrefs(_info.floorPrefab, FLOOR_LABEL, this);
            EditorUtility.SavePrefs(_info.playerPrefab, PLAYER_LABEL, this);
        }

        #endregion Explicit Interface

        private const int ROW_MAX = 20;
        private const int COLUMN_MAX = 20;
        private const string WALL_LABEL = "Wall Prefab";
        private const string FLOOR_LABEL = "Floor Prefab";
        private const string PLAYER_LABEL = "Player Prefab";
        private const string EXIT_LABEL = "Exit Prefab";

        private readonly GroundInfo _info;
        private readonly IFactory<SceneContext> _scFactory;
        private readonly IFactory<SceneContext, UnityEngine.Object, Player> _playerFactory;
        private readonly IFactory<int, int, GameObject> _startFactory, _exitFactory;
        private readonly IFactory<Canvas> _uiFactory;
        private readonly IFactory<int, int, UnityEngine.Object, IEnumerable<GameObject>> _floorFactory, _wallFactory;

        public GoundMaker(GroundInfo info,
                          IFactory<SceneContext> scFactory,
                          IFactory<SceneContext, UnityEngine.Object, Player> playerFactory,
                          IFactory<Canvas> uiFactory,
                          [Inject(Id ="start")]
                          IFactory<int, int, GameObject> startFactory,
                          [Inject(Id ="exit")]
                          IFactory<int, int, GameObject> exitFactory,
                          [Inject(Id ="floor")]
                          IFactory<int, int, UnityEngine.Object, IEnumerable<GameObject>> floorFactory,
                          [Inject(Id ="wall")]
                          IFactory<int, int, UnityEngine.Object, IEnumerable<GameObject>> wallFactory)
        {
            _info = info;

            _scFactory = scFactory;
            _playerFactory = playerFactory;
            _uiFactory = uiFactory;

            _startFactory = startFactory;
            _exitFactory = exitFactory;
            _floorFactory = floorFactory;
            _wallFactory = wallFactory;
        }

        private void GUIRender_RowColumn()
        {
            _info.row = EditorGUILayout.IntSlider("Row : ", _info.row, 2, ROW_MAX);
            _info.column = EditorGUILayout.IntSlider("Column : ", _info.column, 2, COLUMN_MAX);
        }

        private void GUIRender_ButtonCreate()
        {
            GUI.enabled = _info.floorPrefab.IsValid() && _info.wallPrefab.IsValid();
            if (GUILayout.Button("Create"))
            {
                var sc = _scFactory.Create();
                _playerFactory.Create(sc, _info.playerPrefab);
                _startFactory.Create(_info.row, _info.column);
                _exitFactory.Create(_info.row, _info.column);
                _uiFactory.Create();

                _floorFactory.Create(_info.row, _info.column, _info.floorPrefab);
                _wallFactory.Create(_info.row, _info.column, _info.wallPrefab);
            }
            GUI.enabled = true;
        }

        private void GUIRender_Prefabs()
        {
            if (EditorUtility.FoldOut("Prefabs", this))
            {
                using (EditorUtility.Contents())
                {
                    _info.wallPrefab = EditorGUILayout.ObjectField(WALL_LABEL, _info.wallPrefab, typeof(UnityEngine.Object), false);
                    _info.floorPrefab = EditorGUILayout.ObjectField(FLOOR_LABEL, _info.floorPrefab, typeof(UnityEngine.Object), false);
                    _info.playerPrefab = EditorGUILayout.ObjectField(PLAYER_LABEL, _info.playerPrefab, typeof(UnityEngine.Object), false);
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
            public UnityEngine.Object playerPrefab;
        }
    }

    public class GridPositionCalculator
    {
        private const int UNIT = 1;
        private const float HALF_UNIT = UNIT * 0.5f;
        private readonly int _row, _column;
        private readonly float _left, _bottom;

        public int lastRow { get { return _row - 1; } }
        public int lastColumn { get { return _column - 1; } }
        public Vector3 leftBottom { get { return new Vector3(_left, _bottom); } }
        public Vector3 rightTop { get { return Calc(lastRow, lastColumn); } }

        public GridPositionCalculator(int row, int column)
        {
            _row = row;
            _column = column;

            _left = CalcLeft(_row);
            _bottom = CalcBottom(_column);
        }

        private float CalcLeft(int row)
        {
            return HALF_UNIT - (row * HALF_UNIT);
        }

        private float CalcBottom(int column)
        {
            return HALF_UNIT - (column * HALF_UNIT);
        }

        public Vector3 Calc(int i, int j)
        {
            Debug.Assert(i.IsLess(_row));
            Debug.Assert(j.IsLess(_column));
            return new Vector3(_left + (i * UNIT), _bottom + (j * UNIT));
        }
    }
}