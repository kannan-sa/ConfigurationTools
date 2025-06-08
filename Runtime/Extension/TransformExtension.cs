using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension
{
    public static Vector3 GetAxisDot(this Transform from, Transform to) {
        Vector3 result = Vector3.zero;
        Vector3 direction = Vector3.Normalize(to.position - from.position);
        result.y = Vector3.Dot(from.up, direction);
        result.x = Vector3.Dot(from.right, direction);
        result.z = Vector3.Dot(from.forward, direction);
        return result;
    }

    public static void ClearChildren(this Transform transform)
    {
        for (int i = transform.childCount - 1; i >= 0 ; i--)
        {
            Transform child = transform.GetChild(i);
#if UNITY_EDITOR
            GameObject.DestroyImmediate(child.gameObject);
#else
            GameObject.Destroy(child.gameObject);
#endif
        }
    }
}
