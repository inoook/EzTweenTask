using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Ez;

[CustomPropertyDrawer(typeof(TweenAnimChildren))]
public class TweenAnimChildrenDrawer : PropertyDrawer
{
    Color playingColor = Color.green;
    //Color disableColor = Color.grey;

    public override void OnGUI(Rect drawRect, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(drawRect, label, property);

        var isPlayingChainsProperty = property.FindPropertyRelative("isPlayingChains");
        var isPlayingParallelsProperty = property.FindPropertyRelative("isPlayingParallels");

        GUI.contentColor = isPlayingChainsProperty.boolValue ? playingColor : Color.white;
        drawRect.y += EditorGUIUtility.singleLineHeight + 2f;
        EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("chains"), new GUIContent("chains"), true);

        float childGroupH = EditorGUI.GetPropertyHeight(property.FindPropertyRelative("chains"), true);
        drawRect.y += childGroupH;

        GUI.contentColor = isPlayingParallelsProperty.boolValue ? playingColor : Color.white;
        drawRect.y += EditorGUIUtility.singleLineHeight + 2f;
        EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("parallels"), new GUIContent("parallels"), true);

        GUI.contentColor = Color.white;
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var padding = 5;
        var lineH = EditorGUIUtility.singleLineHeight + 4;
        var height = 0f;

        var chainsGroupProperty = property.FindPropertyRelative("chains");
        var parallelsGroupProperty = property.FindPropertyRelative("parallels");

        float childGroupH = EditorGUI.GetPropertyHeight(chainsGroupProperty, true);
        float parallelsGroupH = EditorGUI.GetPropertyHeight(parallelsGroupProperty, true);
        height += childGroupH + parallelsGroupH + padding;

        height += lineH * 1.5f;
        return height + padding;
    }
}
