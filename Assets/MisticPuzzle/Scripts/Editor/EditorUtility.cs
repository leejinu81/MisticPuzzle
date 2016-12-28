using System;
using UnityEditor;
using UnityEngine;

namespace Lonely.Editor
{
    public interface IHasBounds : IDisposable
    {
        Rect Bounds { get; }
    }

    public class EditorUtility
    {
        #region HorizontalLayout

        private class HorizontalLayout : IHasBounds
        {
            #region Explicit Interface

            Rect IHasBounds.Bounds { get { return _bounds; } }

            void IDisposable.Dispose() { EditorGUILayout.EndHorizontal(); }

            #endregion Explicit Interface

            private readonly Rect _bounds;

            public HorizontalLayout(params GUILayoutOption[] options)
            {
                _bounds = EditorGUILayout.BeginHorizontal(options);
            }

            public HorizontalLayout(GUIStyle style, params GUILayoutOption[] options)
            {
                _bounds = EditorGUILayout.BeginHorizontal(style, options);
            }
        }

        public static IHasBounds Horizontal(params GUILayoutOption[] options) { return new HorizontalLayout(options); }

        public static IHasBounds Horizontal(GUIStyle style, params GUILayoutOption[] options) { return new HorizontalLayout(style, options); }

        #endregion HorizontalLayout

        #region VerticalLayout

        private class VerticalLayout : IHasBounds
        {
            #region Explicit Interface

            Rect IHasBounds.Bounds { get { return _bounds; } }

            void IDisposable.Dispose() { EditorGUILayout.EndHorizontal(); }

            #endregion Explicit Interface

            private readonly Rect _bounds;

            public VerticalLayout(params GUILayoutOption[] options)
            {
                _bounds = EditorGUILayout.BeginVertical(options);
            }

            public VerticalLayout(GUIStyle style, params GUILayoutOption[] options)
            {
                _bounds = EditorGUILayout.BeginVertical(style, options);
            }
        }

        public static IHasBounds Vertical(params GUILayoutOption[] options) { return new VerticalLayout(options); }

        public static IHasBounds Vertical(GUIStyle style, params GUILayoutOption[] options) { return new VerticalLayout(style, options); }

        #endregion VerticalLayout

        #region FoldOut

        public static bool FoldOut(string title, object keyObj)
        {
            return FoldOut(title, keyObj.GetType().FullName + title);
        }

        public static bool FoldOut(string title, string prefsKey, bool startFold = true)
        {
            bool prefsBool = EditorPrefs.GetBool(prefsKey, startFold);
            bool flag = EditorGUILayout.Foldout(prefsBool, title);
            if (prefsBool != flag)
            {
                EditorPrefs.SetBool(prefsKey, flag);
            }
            return flag;
        }

        #endregion FoldOut

        #region Contents

        private class ContentsHelper : IDisposable
        {
            #region Explicit Interface

            void IDisposable.Dispose() { _decorated.Dispose(); }

            #endregion Explicit Interface

            private readonly IDisposable _decorated;

            private ContentsHelper(IDisposable decorated)
            {
                _decorated = decorated;
            }

            public static IDisposable Create()
            {
                return new ContentsHelper(Vertical("HelpBox", new GUILayoutOption[0]));
            }
        }

        public static IDisposable Contents() { return ContentsHelper.Create(); }

        #endregion Contents

        #region Save / Load Prefs

        public static T LoadAssetInPrefs<T>(string prefsKey, object keyObj)
            where T : UnityEngine.Object
        {
            string key = MakeKey(prefsKey, keyObj);
            string path = EditorPrefs.GetString(key);
            if (string.IsNullOrEmpty(path))
                return null;

            return AssetDatabase.LoadAssetAtPath<T>(path);
        }

        public static void SavePrefs(bool value, string prefsKey, object keyObj)
        {
            EditorPrefs.SetBool(MakeKey(prefsKey, keyObj), value);
        }

        public static void SavePrefs(UnityEngine.Object asset, string prefsKey, object keyObj)
        {
            if (null != asset)
            {
                string prefabPath = AssetDatabase.GetAssetPath(asset);
                EditorPrefs.SetString(MakeKey(prefsKey, keyObj), prefabPath);
            }
        }

        private static string MakeKey(string prefsKey, object keyObj)
        {
            return keyObj.GetType().FullName + "_" + prefsKey;
        }

        #endregion Save / Load Prefs
    }
}