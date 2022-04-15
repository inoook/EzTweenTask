using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITweenable<T>
{
    T Lerp(T from, T to, float v);
}

public static class Vector3Ext
{
    public static Vector3 Lerp(this Vector3 self, Vector3 to, float v)
    {
        return Vector3.LerpUnclamped(self, to, v);
    }
}
public static class ColorExt
{
    public static Color Lerp(this Color self, Color to, float v)
    {
        return Color.Lerp(self, to, v);
    }
}
