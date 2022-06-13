using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Ez;

// ----------
[CustomPropertyDrawer(typeof(TweenObj))]
public class TweenObjDrawer : PropertyDrawer
{
    Color playingColor = Color.green;
    Color disableColor = Color.grey;

    public override void OnGUI(Rect drawRect, SerializedProperty property, GUIContent label)
    {
        //PropertyDrawerUtility.DrawDefaultGUI(drawRect, property, label);
        //return;
        // List用に1つのプロパティであることを示すためPropertyScopeで囲む
        //using (new EditorGUI.PropertyScope(drawRect, label, property))
        {
            EditorGUI.BeginProperty(drawRect, label, property);
            drawRect.height = EditorGUIUtility.singleLineHeight;

            var playingProperty = property.FindPropertyRelative("isPlaying");
            var playingChildGroupProperty = property.FindPropertyRelative("isPlayingChildGroup");
            var enableProperty = property.FindPropertyRelative("enable");

            GUI.contentColor = playingProperty.boolValue ? playingColor : enableProperty.boolValue ? Color.white : disableColor;
            EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("enable"), new GUIContent("enable"), false);

            var actionTypeProperty = property.FindPropertyRelative("type");
            if(actionTypeProperty == null) {
                Debug.LogWarning("No");
                return; }
            if (actionTypeProperty.intValue == (int)TweenObj.TYPE.__DELAY)
            {
                // Delay
                drawRect.y += EditorGUIUtility.singleLineHeight + 2f;
                EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("delay"), new GUIContent("delay"), false);

                drawRect.y += EditorGUIUtility.singleLineHeight + 2f;
                EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("type"), new GUIContent("type"), false);
            }
            else
            {
                // Delay以外

                drawRect.y += EditorGUIUtility.singleLineHeight + 2f;
                EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("target"), new GUIContent("target"), false);

                drawRect.y += EditorGUIUtility.singleLineHeight + 2f;
                EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("time"), new GUIContent("time"), false);

                drawRect.y += EditorGUIUtility.singleLineHeight + 2f;
                EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("ezEaseType"), new GUIContent("easeType"), false);

                var easeTypeProperty = property.FindPropertyRelative("ezEaseType");
                //if (easeTypeProperty.enumValueIndex == (int)(EzEaseType.AnimationCurve))
                if (easeTypeProperty.intValue == (int)(EzEaseType.AnimationCurve))
                    {
                    drawRect.y += EditorGUIUtility.singleLineHeight + 2f;
                    EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("animationCurve"), new GUIContent("curve"), false);
                }

                drawRect.y += EditorGUIUtility.singleLineHeight + 2f;
                //var actionTypeProperty = property.FindPropertyRelative("type");
                EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("type"), new GUIContent("type"), false);
                //actionTypeProperty.enumValueIndex = EditorGUI.Popup(drawRect, "type", actionTypeProperty.enumValueIndex, System.Enum.GetNames(typeof(EzTweenBehaviour.TYPE)));
                //actionTypeProperty.enumValueIndex = (int)((EzTweenBehaviour.TYPE)(EditorGUI.EnumPopup(drawRect, "type", (EzTweenBehaviour.TYPE)actionTypeProperty.enumValueIndex)));

                drawRect.y += EditorGUIUtility.singleLineHeight + 2f;
                EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("animType"), new GUIContent("animType"), false);

                var animTypeProperty = property.FindPropertyRelative("animType");
                //if (animTypeProperty.enumValueIndex == (int)TweenAnimBehaviour.AnimType.NORMAL)
                if (animTypeProperty.intValue == (int)TweenObj.AnimType.NORMAL)
                {
                    drawRect.y += EditorGUIUtility.singleLineHeight + 2f;

                    // typeによるフィールド表示変更
                    switch (actionTypeProperty.intValue)
                    {
                        case (int)TweenObj.TYPE.POSITION:
                        case (int)TweenObj.TYPE.POSITION_RELATIVE:
                            EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("to_Vec3"), new GUIContent("pos"), false);
                            break;
                        case (int)TweenObj.TYPE.SCALE:
                        case (int)TweenObj.TYPE.SCALE_RELATIVE:
                            EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("to_Vec3"), new GUIContent("scale"), false);
                            break;
                        case (int)TweenObj.TYPE.COLOR:
                            EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("to_Color"), new GUIContent("color"), false);
                            break;
                        case (int)TweenObj.TYPE.ALPHA:
                            EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("to_V"), new GUIContent("alpha"), false);
                            break;

                        default: break;
                    }
                }
            }

            //GUI.contentColor = Color.white;
            //drawRect.y += EditorGUIUtility.singleLineHeight + 8f;
            //GUI.contentColor = playingChildGroupProperty.boolValue ? playingColor : Color.white;
            //EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("childGroup"), new GUIContent("group"), true);

            GUI.contentColor = Color.white;

            EditorGUI.EndProperty();
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var padding = 5;
        var lineH = EditorGUIUtility.singleLineHeight + 4;
        var height = 0f;

        //var childGroupProperty = property.FindPropertyRelative("childGroup");
        //float childGroupH = EditorGUI.GetPropertyHeight(childGroupProperty, true);
        //height += childGroupH + padding;

        var actionTypeProperty = property.FindPropertyRelative("type");
        //bool isDelay = (TweenAnimBehaviour.TYPE)actionTypeProperty.enumValueIndex == TweenAnimBehaviour.TYPE.__DELAY;
        bool isDelay = actionTypeProperty.intValue == (int)TweenObj.TYPE.__DELAY;
        // Delayの時のサイズ
        if (isDelay)
        {
            height += lineH * 3.25f + padding;
            return height;
        }

        // 通常時
        var animTypeProperty = property.FindPropertyRelative("animType");
        //bool isNormal = (TweenAnimBehaviour.AnimType)animTypeProperty.enumValueIndex == TweenAnimBehaviour.AnimType.NORMAL;
        bool isNormal = animTypeProperty.intValue == (int)TweenObj.AnimType.NORMAL;

        int index = isNormal ? 1 : 0; 
        var easeTypeProperty = property.FindPropertyRelative("ezEaseType");
        if (easeTypeProperty.intValue == (int)(EzEaseType.AnimationCurve))
        {
            height += lineH*(7 + index);
        }
        else
        {
            height += lineH * (6 + index);
        }

        return height + padding;
    }
}