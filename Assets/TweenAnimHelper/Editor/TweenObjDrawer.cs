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
        //using (new EditorGUI.PropertyScope(position, label, property))
        {
            EditorGUI.BeginProperty(drawRect, label, property);
            drawRect.height = EditorGUIUtility.singleLineHeight;

            var playingProperty = property.FindPropertyRelative("isPlaying");
            var enableProperty = property.FindPropertyRelative("enable");

            GUI.contentColor = playingProperty.boolValue ? playingColor : enableProperty.boolValue ? Color.white : disableColor;
            EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("enable"), new GUIContent("enable"), false);

            drawRect.y += EditorGUIUtility.singleLineHeight + 2f;
            EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("target"), new GUIContent("target"), false);

            drawRect.y += EditorGUIUtility.singleLineHeight + 2f;
            EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("delay"), new GUIContent("delay"), false);

            drawRect.y += EditorGUIUtility.singleLineHeight + 2f;
            EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("time"), new GUIContent("time"), false);

            drawRect.y += EditorGUIUtility.singleLineHeight + 2f;
            EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("ezEaseType"), new GUIContent("easeType"), false);


            var easeTypeProperty = property.FindPropertyRelative("ezEaseType");
            if (easeTypeProperty.enumValueIndex == (int)(EzEaseType.AnimationCurve))
            {
                drawRect.y += EditorGUIUtility.singleLineHeight + 2f;
                EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("animationCurve"), new GUIContent("curve"), false);
            }

            drawRect.y += EditorGUIUtility.singleLineHeight + 2f;
            var actionTypeProperty = property.FindPropertyRelative("type");
            EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("type"), new GUIContent("type"), false);
            //actionTypeProperty.enumValueIndex = EditorGUI.Popup(drawRect, "type", actionTypeProperty.enumValueIndex, System.Enum.GetNames(typeof(EzTweenBehaviour.TYPE)));
            //actionTypeProperty.enumValueIndex = (int)((EzTweenBehaviour.TYPE)(EditorGUI.EnumPopup(drawRect, "type", (EzTweenBehaviour.TYPE)actionTypeProperty.enumValueIndex)));


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

            GUI.contentColor = Color.white;

            EditorGUI.EndProperty();
        }
    }

    //public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //{
    //    // List用に1つのプロパティであることを示すためPropertyScopeで囲む
    //    using (new EditorGUI.PropertyScope(position, label, property))
    //    {
    //        // 0指定だとReorderableListのドラッグと被るのでLineHeightを指定
    //        position.height = EditorGUIUtility.singleLineHeight;

    //        var drawRect = new Rect(position)
    //        {
    //            y = position.y
    //        };

    //        var playingProperty = property.FindPropertyRelative("isPlaying");
    //        var enableProperty = property.FindPropertyRelative("enable");

    //        GUI.contentColor = playingProperty.boolValue ? playingColor : enableProperty.boolValue ? Color.white : disableColor;
    //        enableProperty.boolValue = EditorGUI.Toggle(drawRect, "enable", enableProperty.boolValue);

    //        drawRect.y += EditorGUIUtility.singleLineHeight + 2f;

    //        var targetProperty = property.FindPropertyRelative("target");
    //        targetProperty.objectReferenceValue = EditorGUI.ObjectField(drawRect, "target", targetProperty.objectReferenceValue, typeof(GameObject), true);

    //        drawRect.y += EditorGUIUtility.singleLineHeight + 2f;

    //        var delayProperty = property.FindPropertyRelative("delay");
    //        delayProperty.floatValue = EditorGUI.FloatField(drawRect, "delay", delayProperty.floatValue);

    //        drawRect.y += EditorGUIUtility.singleLineHeight + 2f;

    //        var timeProperty = property.FindPropertyRelative("time");
    //        timeProperty.floatValue = EditorGUI.FloatField(drawRect, "time", timeProperty.floatValue);

    //        drawRect.y += EditorGUIUtility.singleLineHeight + 2f;

    //        var easeTypeProperty = property.FindPropertyRelative("ezEaseType");
    //        easeTypeProperty.enumValueIndex = EditorGUI.Popup(drawRect, "easeType", easeTypeProperty.enumValueIndex, System.Enum.GetNames(typeof(EzEaseType)));

    //        drawRect.y += EditorGUIUtility.singleLineHeight + 2f;

    //        if (easeTypeProperty.enumValueIndex == (int)(EzEaseType.AnimationCurve))
    //        {
    //            var animCurveProperty = property.FindPropertyRelative("animationCurve");
    //            animCurveProperty.animationCurveValue = EditorGUI.CurveField(drawRect, "curve", animCurveProperty.animationCurveValue);

    //            drawRect.y += EditorGUIUtility.singleLineHeight + 2f;
    //        }

    //        var actionTypeProperty = property.FindPropertyRelative("type");
    //        actionTypeProperty.enumValueIndex = EditorGUI.Popup(drawRect, "type", actionTypeProperty.enumValueIndex, System.Enum.GetNames(typeof(EzTweenBehaviour.TYPE)));


    //        switch ((EzTweenBehaviour.TYPE)actionTypeProperty.enumValueIndex)
    //        {
    //            case EzTweenBehaviour.TYPE.POSITION:
    //            case EzTweenBehaviour.TYPE.POSITION_RELATIVE:
    //                var _property = property.FindPropertyRelative("to_Vec3");
    //                var rect = new Rect(drawRect)
    //                {
    //                    y = drawRect.y + EditorGUIUtility.singleLineHeight + 2f,
    //                };
    //                _property.vector3Value = EditorGUI.Vector3Field(rect, "pos", _property.vector3Value);
    //                break;
    //            case EzTweenBehaviour.TYPE.SCALE:
    //            case EzTweenBehaviour.TYPE.SCALE_RELATIVE:
    //                var _propertyScale = property.FindPropertyRelative("to_Vec3");
    //                var rectScale = new Rect(drawRect)
    //                {
    //                    y = drawRect.y + EditorGUIUtility.singleLineHeight + 2f,
    //                };
    //                _propertyScale.vector3Value = EditorGUI.Vector3Field(rectScale, "scale", _propertyScale.vector3Value);
    //                break;
    //            case EzTweenBehaviour.TYPE.COLOR:
    //                var _propertyColor = property.FindPropertyRelative("to_Color");
    //                var rectColor = new Rect(drawRect)
    //                {
    //                    y = drawRect.y + EditorGUIUtility.singleLineHeight + 2f,
    //                    height = (EditorGUIUtility.singleLineHeight * 1)
    //                };
    //                _propertyColor.colorValue = EditorGUI.ColorField(rectColor, "color", _propertyColor.colorValue);
    //                break;
    //            case EzTweenBehaviour.TYPE.ALPHA:
    //                var _propertyAlpha = property.FindPropertyRelative("to_V");
    //                var rectAlpha = new Rect(drawRect)
    //                {
    //                    y = drawRect.y + EditorGUIUtility.singleLineHeight + 2f,
    //                    height = (EditorGUIUtility.singleLineHeight * 1)
    //                };
    //                _propertyAlpha.floatValue = EditorGUI.FloatField(rectAlpha, "alpha", _propertyAlpha.floatValue);
    //                break;

    //            default: break;
    //        }

    //        GUI.contentColor = Color.white;
    //    }
    //}

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var height = EditorGUIUtility.singleLineHeight + 4;

        var actionTypeProperty = property.FindPropertyRelative("type");
        bool isRevert = actionTypeProperty.enumValueIndex == (int)(TweenAnimBehaviour.TYPE.__POSITION_REVERT)
            || actionTypeProperty.enumValueIndex == (int)(TweenAnimBehaviour.TYPE.__SCALE_REVERT)
            || actionTypeProperty.enumValueIndex == (int)(TweenAnimBehaviour.TYPE.__ALPHA_REVERT)
            || actionTypeProperty.enumValueIndex == (int)(TweenAnimBehaviour.TYPE.__COLOR_REVERT)
            ;
        int index = isRevert ? -1 : 0; 

        var easeTypeProperty = property.FindPropertyRelative("ezEaseType");
        if (easeTypeProperty.enumValueIndex == (int)(EzEaseType.AnimationCurve))
        {
            height *= (8 + index);
        }
        else
        {
            height *= (7 + index);
        }
        return height;
    }
}