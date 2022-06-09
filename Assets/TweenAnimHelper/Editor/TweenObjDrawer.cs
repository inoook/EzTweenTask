using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Ez;

// ----------
// https://gomafrontier.com/unity/4553
[CustomPropertyDrawer(typeof(TweenAnimBehaviour.TweenObj))]
public class TweenObjDrawer : PropertyDrawer
{
    Color playingColor = Color.green;
    Color disableColor = Color.grey;

    public override void OnGUI(Rect drawRect, SerializedProperty property, GUIContent label)
    {
        // List用に1つのプロパティであることを示すためPropertyScopeで囲む
        //using (new EditorGUI.PropertyScope(drawRect, label, property))
        {
            EditorGUI.BeginProperty(drawRect, label, property);
            drawRect.height = EditorGUIUtility.singleLineHeight;

            var playingProperty = property.FindPropertyRelative("isPlaying");
            var enableProperty = property.FindPropertyRelative("enable");

            GUI.contentColor = playingProperty.boolValue ? playingColor : enableProperty.boolValue ? Color.white : disableColor;
            EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("enable"), new GUIContent("enable"), false);

            var actionTypeProperty = property.FindPropertyRelative("type");
            if(actionTypeProperty == null) {
                Debug.LogWarning("No");
                return; }
            if ((TweenAnimBehaviour.TYPE)actionTypeProperty.enumValueIndex == TweenAnimBehaviour.TYPE.__DELAY)
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
                if (animTypeProperty.intValue == (int)TweenAnimBehaviour.AnimType.NORMAL)
                    {
                    drawRect.y += EditorGUIUtility.singleLineHeight + 2f;

                    switch ((TweenAnimBehaviour.TYPE)actionTypeProperty.enumValueIndex)
                    {
                        case TweenAnimBehaviour.TYPE.POSITION:
                        case TweenAnimBehaviour.TYPE.POSITION_RELATIVE:
                            EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("to_Vec3"), new GUIContent("pos"), false);
                            break;
                        case TweenAnimBehaviour.TYPE.SCALE:
                        case TweenAnimBehaviour.TYPE.SCALE_RELATIVE:
                            EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("to_Vec3"), new GUIContent("scale"), false);
                            break;
                        case TweenAnimBehaviour.TYPE.COLOR:
                            EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("to_Color"), new GUIContent("color"), false);
                            break;
                        case TweenAnimBehaviour.TYPE.ALPHA:
                            EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("to_V"), new GUIContent("alpha"), false);
                            break;

                        default: break;
                    }
                }
            }

            GUI.contentColor = Color.white;

            //drawRect.y += EditorGUIUtility.singleLineHeight + 2f;
            //EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("childGroup"), new GUIContent("group"), true);

            EditorGUI.EndProperty();
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var padding = 5;
        var height = EditorGUIUtility.singleLineHeight+4;

        var actionTypeProperty = property.FindPropertyRelative("type");
        //bool isDelay = (TweenAnimBehaviour.TYPE)actionTypeProperty.enumValueIndex == TweenAnimBehaviour.TYPE.__DELAY;
        bool isDelay = actionTypeProperty.enumValueIndex == (int)TweenAnimBehaviour.TYPE.__DELAY;
        // Delayの時のサイズ
        if (isDelay)
        {
            return height * 3.25f + padding;
        }

        // 通常時
        var animTypeProperty = property.FindPropertyRelative("animType");
        //bool isNormal = (TweenAnimBehaviour.AnimType)animTypeProperty.enumValueIndex == TweenAnimBehaviour.AnimType.NORMAL;
        bool isNormal = animTypeProperty.enumValueIndex == (int)TweenAnimBehaviour.AnimType.NORMAL;

        int index = isNormal ? 1 : 0; 
        var easeTypeProperty = property.FindPropertyRelative("ezEaseType");
        if (easeTypeProperty.enumValueIndex == (int)(EzEaseType.AnimationCurve))
        {
            height *= (7 + index);
        }
        else
        {
            height *= (6 + index);
        }
        return height + padding;
    }
}