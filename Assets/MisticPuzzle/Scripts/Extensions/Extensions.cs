using System;
using UnityEngine;

namespace Extension
{
    public static class NullCheckExtensions
    {
        public static bool IsNull<T>(this T root) where T : class
        {
            return root == null;
        }

        public static bool IsNull<T>(this T? obj) where T : struct
        {
            return !obj.HasValue;
        }

        public static bool IsValid<T>(this T root) where T : class
        {
            return root != null;
        }
    }

    public static class ValueTypeComparer
    {
        public static bool IsGreater<T>(this T thisT, T other) where T : IComparable
        {
            return thisT.CompareTo(other) > 0;
        }

        public static bool IsGreaterOrEqual<T>(this T thisT, T other) where T : IComparable
        {
            return IsGreater(thisT, other) || Equals(thisT, other);
        }

        public static bool IsLess<T>(this T thisT, T other) where T : IComparable
        {
            return thisT.CompareTo(other) < 0;
        }

        public static bool IsLessOrEqual<T>(this T thisT, T other) where T : IComparable
        {
            return IsLess(thisT, other) || Equals(thisT, other);
        }

        public static bool IsTrue(this bool thisBool) { return thisBool == true; }

        public static bool IsFalse(this bool thisBool) { return thisBool == false; }
    }

    public static class GameObjectExtensions
    {
        #region Position

        // Get
        public static Vector3 X(this GameObject go) { return go.transform.X(); }

        public static Vector3 Y(this GameObject go) { return go.transform.Y(); }

        public static Vector3 Z(this GameObject go) { return go.transform.Z(); }

        public static Vector3 XY(this GameObject go) { return go.transform.XY(); }

        public static Vector3 XZ(this GameObject go) { return go.transform.XZ(); }

        public static Vector3 YZ(this GameObject go) { return go.transform.YZ(); }

        public static Vector3 XYZ(this GameObject go) { return go.transform.XYZ(); }

        // Set Vector3
        public static void SetX(this GameObject go, Vector3 v) { go.transform.SetX(v); }

        public static void SetY(this GameObject go, Vector3 v) { go.transform.SetY(v); }

        public static void SetZ(this GameObject go, Vector3 v) { go.transform.SetZ(v); }

        public static void SetXY(this GameObject go, Vector3 v) { go.transform.SetXY(v); }

        public static void SetXZ(this GameObject go, Vector3 v) { go.transform.SetXZ(v); }

        public static void SetYZ(this GameObject go, Vector3 v) { go.transform.SetYZ(v); }

        public static void SetXYZ(this GameObject go, Vector3 v) { go.transform.SetXYZ(v); }

        // Set float
        public static void SetX(this GameObject go, float x) { go.transform.SetX(x); }

        public static void SetY(this GameObject go, float y) { go.transform.SetY(y); }

        public static void SetZ(this GameObject go, float z) { go.transform.SetZ(z); }

        public static void SetXY(this GameObject go, float x, float y) { go.transform.SetXY(x, y); }

        public static void SetXZ(this GameObject go, float x, float z) { go.transform.SetXZ(x, z); }

        public static void SetYZ(this GameObject go, float y, float z) { go.transform.SetYZ(y, z); }

        public static void SetXYZ(this GameObject go, float x, float y, float z) { go.transform.SetXYZ(x, y, z); }

        #endregion Position

        #region Local Position

        // Get
        public static Vector3 LocalX(this GameObject go) { return go.transform.LocalX(); }

        public static Vector3 LocalY(this GameObject go) { return go.transform.LocalY(); }

        public static Vector3 LocalZ(this GameObject go) { return go.transform.LocalZ(); }

        public static Vector3 LocalXY(this GameObject go) { return go.transform.LocalXY(); }

        public static Vector3 LocalXZ(this GameObject go) { return go.transform.LocalXZ(); }

        public static Vector3 LocalYZ(this GameObject go) { return go.transform.LocalYZ(); }

        public static Vector3 LocalXYZ(this GameObject go) { return go.transform.LocalXYZ(); }

        // Set Vector3
        public static void LocalSetX(this GameObject go, Vector3 v) { go.transform.SetLocalX(v); }

        public static void LocalSetY(this GameObject go, Vector3 v) { go.transform.SetLocalY(v); }

        public static void LocalSetZ(this GameObject go, Vector3 v) { go.transform.SetLocalZ(v); }

        public static void LocalSetXY(this GameObject go, Vector3 v) { go.transform.SetLocalXY(v); }

        public static void LocalSetXZ(this GameObject go, Vector3 v) { go.transform.SetLocalXZ(v); }

        public static void LocalSetYZ(this GameObject go, Vector3 v) { go.transform.SetLocalYZ(v); }

        public static void LocalSetXYZ(this GameObject go, Vector3 v) { go.transform.SetLocalXYZ(v); }

        // Set float
        public static void LocalSetX(this GameObject go, float x) { go.transform.SetLocalX(x); }

        public static void LocalSetY(this GameObject go, float y) { go.transform.SetLocalY(y); }

        public static void LocalSetZ(this GameObject go, float z) { go.transform.SetLocalZ(z); }

        public static void LocalSetXY(this GameObject go, float x, float y) { go.transform.SetLocalXY(x, y); }

        public static void LocalSetXZ(this GameObject go, float x, float z) { go.transform.SetLocalXZ(x, z); }

        public static void LocalSetYZ(this GameObject go, float y, float z) { go.transform.SetLocalYZ(y, z); }

        public static void LocalSetXYZ(this GameObject go, float x, float y, float z) { go.transform.SetLocalXYZ(x, y, z); }

        #endregion Local Position

        #region Quaternion

        public static Quaternion Rot(this GameObject go) { return go.transform.Rot(); }

        public static void SetRot(this GameObject go, Quaternion q) { go.transform.SetRot(q); }

        #endregion Quaternion

        #region Local Quaternion

        public static Quaternion LocalRot(this GameObject go) { return go.transform.localRotation; }

        public static void SetLocalRot(this GameObject go, Quaternion q) { go.transform.localRotation = q; }

        #endregion Local Quaternion
    }

    public static class ComponentExtensions
    {
        #region Position

        // Get
        public static Vector3 X(this Component com) { return com.transform.X(); }

        public static Vector3 Y(this Component com) { return com.transform.Y(); }

        public static Vector3 Z(this Component com) { return com.transform.Z(); }

        public static Vector3 XY(this Component com) { return com.transform.XY(); }

        public static Vector3 XZ(this Component com) { return com.transform.XZ(); }

        public static Vector3 YZ(this Component com) { return com.transform.YZ(); }

        public static Vector3 XYZ(this Component com) { return com.transform.XYZ(); }

        // Set Vector3
        public static void SetX(this Component com, Vector3 v) { com.transform.SetX(v); }

        public static void SetY(this Component com, Vector3 v) { com.transform.SetY(v); }

        public static void SetZ(this Component com, Vector3 v) { com.transform.SetZ(v); }

        public static void SetXY(this Component com, Vector3 v) { com.transform.SetXY(v); }

        public static void SetXZ(this Component com, Vector3 v) { com.transform.SetXZ(v); }

        public static void SetYZ(this Component com, Vector3 v) { com.transform.SetYZ(v); }

        public static void SetXYZ(this Component com, Vector3 v) { com.transform.SetXYZ(v); }

        // Set float
        public static void SetX(this Component com, float x) { com.transform.SetX(x); }

        public static void SetY(this Component com, float y) { com.transform.SetY(y); }

        public static void SetZ(this Component com, float z) { com.transform.SetZ(z); }

        public static void SetXY(this Component com, float x, float y) { com.transform.SetXY(x, y); }

        public static void SetXZ(this Component com, float x, float z) { com.transform.SetXZ(x, z); }

        public static void SetYZ(this Component com, float y, float z) { com.transform.SetYZ(y, z); }

        public static void SetXYZ(this Component com, float x, float y, float z) { com.transform.SetXYZ(x, y, z); }

        #endregion Position

        #region Local Position

        // Get
        public static Vector3 LocalX(this Component com) { return com.transform.LocalX(); }

        public static Vector3 LocalY(this Component com) { return com.transform.LocalY(); }

        public static Vector3 LocalZ(this Component com) { return com.transform.LocalZ(); }

        public static Vector3 LocalXY(this Component com) { return com.transform.LocalXY(); }

        public static Vector3 LocalXZ(this Component com) { return com.transform.LocalXZ(); }

        public static Vector3 LocalYZ(this Component com) { return com.transform.LocalYZ(); }

        public static Vector3 LocalXYZ(this Component com) { return com.transform.LocalXYZ(); }

        // Set Vector3
        public static void LocalSetX(this Component com, Vector3 v) { com.transform.SetLocalX(v); }

        public static void LocalSetY(this Component com, Vector3 v) { com.transform.SetLocalY(v); }

        public static void LocalSetZ(this Component com, Vector3 v) { com.transform.SetLocalZ(v); }

        public static void LocalSetXY(this Component com, Vector3 v) { com.transform.SetLocalXY(v); }

        public static void LocalSetXZ(this Component com, Vector3 v) { com.transform.SetLocalXZ(v); }

        public static void LocalSetYZ(this Component com, Vector3 v) { com.transform.SetLocalYZ(v); }

        public static void LocalSetXYZ(this Component com, Vector3 v) { com.transform.SetLocalXYZ(v); }

        // Set float
        public static void LocalSetX(this Component com, float x) { com.transform.SetLocalX(x); }

        public static void LocalSetY(this Component com, float y) { com.transform.SetLocalY(y); }

        public static void LocalSetZ(this Component com, float z) { com.transform.SetLocalZ(z); }

        public static void LocalSetXY(this Component com, float x, float y) { com.transform.SetLocalXY(x, y); }

        public static void LocalSetXZ(this Component com, float x, float z) { com.transform.SetLocalXZ(x, z); }

        public static void LocalSetYZ(this Component com, float y, float z) { com.transform.SetLocalYZ(y, z); }

        public static void LocalSetXYZ(this Component com, float x, float y, float z) { com.transform.SetLocalXYZ(x, y, z); }

        #endregion Local Position

        #region Quaternion

        public static Quaternion Rot(this Component com) { return com.transform.Rot(); }

        public static void SetRot(this Component com, Quaternion q) { com.transform.SetRot(q); }

        #endregion Quaternion

        #region Local Quaternion

        public static Quaternion LocalRot(this Component com) { return com.transform.localRotation; }

        public static void SetLocalRot(this Component com, Quaternion q) { com.transform.localRotation = q; }

        #endregion Local Quaternion
    }

    public static class TransformExtensions
    {
        #region Position

        public static Vector3 X(this Transform tr) { return tr.position.X(); }

        public static Vector3 Y(this Transform tr) { return tr.position.Y(); }

        public static Vector3 Z(this Transform tr) { return tr.position.Z(); }

        public static Vector3 XY(this Transform tr) { return tr.position.XY(); }

        public static Vector3 XZ(this Transform tr) { return tr.position.XZ(); }

        public static Vector3 YZ(this Transform tr) { return tr.position.YZ(); }

        public static Vector3 XYZ(this Transform tr) { return tr.position; }

        // Set Vector3
        public static void SetX(this Transform tr, Vector3 v) { tr.position = v.X() + tr.YZ(); }

        public static void SetY(this Transform tr, Vector3 v) { tr.position = v.Y() + tr.XZ(); }

        public static void SetZ(this Transform tr, Vector3 v) { tr.position = v.Z() + tr.XY(); }

        public static void SetXY(this Transform tr, Vector3 v) { tr.position = v.XY() + tr.Z(); }

        public static void SetXZ(this Transform tr, Vector3 v) { tr.position = v.XZ() + tr.Y(); }

        public static void SetYZ(this Transform tr, Vector3 v) { tr.position = v.YZ() + tr.X(); }

        public static void SetXYZ(this Transform tr, Vector3 v) { tr.position = v; }

        // Set float
        public static void SetX(this Transform tr, float x) { tr.position = (Vector3.right * x) + tr.YZ(); }

        public static void SetY(this Transform tr, float y) { tr.position = (Vector3.up * y) + tr.XZ(); }

        public static void SetZ(this Transform tr, float z) { tr.position = (Vector3.forward * z) + tr.XY(); }

        public static void SetXY(this Transform tr, float x, float y) { tr.position = new Vector3(x, y, 0) + tr.Z(); }

        public static void SetXZ(this Transform tr, float x, float z) { tr.position = new Vector3(x, 0, z) + tr.Y(); }

        public static void SetYZ(this Transform tr, float y, float z) { tr.position = new Vector3(0, y, z) + tr.X(); }

        public static void SetXYZ(this Transform tr, float x, float y, float z) { tr.position = new Vector3(x, y, z); }

        #endregion Position

        #region Local Position

        // Get
        public static Vector3 LocalX(this Transform tr) { return tr.localPosition.X(); }

        public static Vector3 LocalY(this Transform tr) { return tr.localPosition.Y(); }

        public static Vector3 LocalZ(this Transform tr) { return tr.localPosition.Z(); }

        public static Vector3 LocalXY(this Transform tr) { return tr.localPosition.XY(); }

        public static Vector3 LocalXZ(this Transform tr) { return tr.localPosition.XZ(); }

        public static Vector3 LocalYZ(this Transform tr) { return tr.localPosition.YZ(); }

        public static Vector3 LocalXYZ(this Transform tr) { return tr.localPosition; }

        // Set Vector3
        public static void SetLocalX(this Transform tr, Vector3 v) { tr.localPosition = v.X() + tr.YZ(); }

        public static void SetLocalY(this Transform tr, Vector3 v) { tr.localPosition = v.Y() + tr.XZ(); }

        public static void SetLocalZ(this Transform tr, Vector3 v) { tr.localPosition = v.Z() + tr.XY(); }

        public static void SetLocalXY(this Transform tr, Vector3 v) { tr.localPosition = v.XY() + tr.Z(); }

        public static void SetLocalXZ(this Transform tr, Vector3 v) { tr.localPosition = v.XZ() + tr.Y(); }

        public static void SetLocalYZ(this Transform tr, Vector3 v) { tr.localPosition = v.YZ() + tr.X(); }

        public static void SetLocalXYZ(this Transform tr, Vector3 v) { tr.localPosition = v; }

        // Set float
        public static void SetLocalX(this Transform tr, float x) { tr.localPosition = (Vector3.right * x) + tr.YZ(); }

        public static void SetLocalY(this Transform tr, float y) { tr.localPosition = (Vector3.up * y) + tr.XZ(); }

        public static void SetLocalZ(this Transform tr, float z) { tr.localPosition = (Vector3.forward * z) + tr.XY(); }

        public static void SetLocalXY(this Transform tr, float x, float y) { tr.localPosition = new Vector3(x, y, 0) + tr.Z(); }

        public static void SetLocalXZ(this Transform tr, float x, float z) { tr.localPosition = new Vector3(x, 0, z) + tr.Y(); }

        public static void SetLocalYZ(this Transform tr, float y, float z) { tr.localPosition = new Vector3(0, y, z) + tr.X(); }

        public static void SetLocalXYZ(this Transform tr, float x, float y, float z) { tr.localPosition = new Vector3(x, y, z); }

        #endregion Local Position

        #region Quaternion

        public static Quaternion Rot(this Transform tr) { return tr.rotation; }

        public static void SetRot(this Transform tr, Quaternion q) { tr.rotation = q; }

        #endregion Quaternion
    }

    public static class RigidbodyExtenstions
    {
        #region Position

        public static Vector3 X(this Rigidbody rb) { return rb.position.X(); }

        public static Vector3 Y(this Rigidbody rb) { return rb.position.Y(); }

        public static Vector3 Z(this Rigidbody rb) { return rb.position.Z(); }

        public static Vector3 XY(this Rigidbody rb) { return rb.position.XY(); }

        public static Vector3 XZ(this Rigidbody rb) { return rb.position.XZ(); }

        public static Vector3 YZ(this Rigidbody rb) { return rb.position.YZ(); }

        public static Vector3 XYZ(this Rigidbody rb) { return rb.position; }

        // Set Vector3
        public static void SetX(this Rigidbody rb, Vector3 v) { rb.position = v.X() + rb.YZ(); }

        public static void SetY(this Rigidbody rb, Vector3 v) { rb.position = v.Y() + rb.XZ(); }

        public static void SetZ(this Rigidbody rb, Vector3 v) { rb.position = v.Z() + rb.XY(); }

        public static void SetXY(this Rigidbody rb, Vector3 v) { rb.position = v.XY() + rb.Z(); }

        public static void SetXZ(this Rigidbody rb, Vector3 v) { rb.position = v.XZ() + rb.Y(); }

        public static void SetYZ(this Rigidbody rb, Vector3 v) { rb.position = v.YZ() + rb.X(); }

        public static void SetXYZ(this Rigidbody rb, Vector3 v) { rb.position = v; }

        // Set float
        public static void SetX(this Rigidbody rb, float x) { rb.position = (Vector3.right * x) + rb.YZ(); }

        public static void SetY(this Rigidbody rb, float y) { rb.position = (Vector3.up * y) + rb.XZ(); }

        public static void SetZ(this Rigidbody rb, float z) { rb.position = (Vector3.forward * z) + rb.XY(); }

        public static void SetXY(this Rigidbody rb, float x, float y) { rb.position = new Vector3(x, y, 0) + rb.Z(); }

        public static void SetXZ(this Rigidbody rb, float x, float z) { rb.position = new Vector3(x, 0, z) + rb.Y(); }

        public static void SetYZ(this Rigidbody rb, float y, float z) { rb.position = new Vector3(0, y, z) + rb.X(); }

        public static void SetXYZ(this Rigidbody rb, float x, float y, float z) { rb.position = new Vector3(x, y, z); }

        #endregion Position
    }

    public static class Rigidbody2DExtenstions
    {
        #region Position

        public static Vector2 X(this Rigidbody2D rb) { return rb.position.X(); }

        public static Vector2 Y(this Rigidbody2D rb) { return rb.position.Y(); }

        public static Vector2 XY(this Rigidbody2D rb) { return rb.position; }

        // Set Vector3
        public static void SetX(this Rigidbody2D rb, Vector2 v) { rb.position = v.X() + rb.Y(); }

        public static void SetY(this Rigidbody2D rb, Vector2 v) { rb.position = v.Y() + rb.X(); }

        public static void SetXY(this Rigidbody2D rb, Vector2 v) { rb.position = v; }

        // Set float
        //public static void SetX(this Rigidbody2D rb, float x) { rb.position = (Vector2.right * x) + rb.Y(); }
        public static void SetX(this Rigidbody2D rb, float x) { rb.position = new Vector2(x, rb.position.y); }

        public static void SetY(this Rigidbody2D rb, float y) { rb.position = (Vector2.up * y) + rb.X(); }

        public static void SetXY(this Rigidbody2D rb, float x, float y) { rb.position = new Vector2(x, y); }

        #endregion Position
    }

    public static class Vector3Extensions
    {
        #region Null Check

        public static bool IsValid(this Vector3 thisVec) { return (thisVec != Vector3.zero); }

        public static bool IsZero(this Vector3 thisVec) { return (thisVec == Vector3.zero); }

        #endregion Null Check

        #region Position

        public static Vector3 X(this Vector3 thisVec) { return Vector3.right * thisVec.x; }

        public static Vector3 Y(this Vector3 thisVec) { return Vector3.up * thisVec.y; }

        public static Vector3 Z(this Vector3 thisVec) { return Vector3.forward * thisVec.z; }

        public static Vector3 XY(this Vector3 thisVec) { return new Vector3(thisVec.x, thisVec.y, 0); }

        public static Vector3 XZ(this Vector3 thisVec) { return new Vector3(thisVec.x, 0, thisVec.z); }

        public static Vector3 YZ(this Vector3 thisVec) { return new Vector3(0, thisVec.y, thisVec.z); }

        #endregion Position
    }

    public static class Vector2Extensions
    {
        #region Null Check

        public static bool IsValid(this Vector2 thisVec) { return (thisVec != Vector2.zero); }

        public static bool IsZero(this Vector2 thisVec) { return (thisVec == Vector2.zero); }

        #endregion Null Check

        #region Position

        public static Vector2 X(this Vector2 thisVec) { return Vector2.right * thisVec.x; }

        public static Vector2 Y(this Vector2 thisVec) { return Vector2.up * thisVec.y; }

        #endregion Position
    }
}