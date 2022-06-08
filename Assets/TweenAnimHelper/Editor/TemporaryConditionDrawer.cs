using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Ez;

// ----------
// https://gomafrontier.com/unity/4553
// https://docs.unity3d.com/jp/current/ScriptReference/EditorGUI.EnumPopup.html
// https://docs.unity3d.com/jp/current/ScriptReference/PropertyDrawer.html
// https://qiita.com/sango/items/b705980ada56ba8ffa04
// https://www.tangledrealitystudios.com/development-tips/create-popup-drowndown-editor-unity/
[CustomPropertyDrawer(typeof(TweenAnimBehaviour.TemporaryCondition),true)]
public class TemporaryConditionDrawer : PropertyDrawer
{

    public override void OnGUI(Rect drawRect, SerializedProperty property, GUIContent label)
    {
        // List用に1つのプロパティであることを示すためPropertyScopeで囲む
        //using (new EditorGUI.PropertyScope(position, label, property))
        {
            EditorGUI.BeginProperty(drawRect, label, property);
            drawRect.height = EditorGUIUtility.singleLineHeight;

            var enableProperty = property.FindPropertyRelative("enable");
            GUI.contentColor = enableProperty.boolValue ? Color.white : Color.grey;

            // enableProperty.boolValue = EditorGUI.Toggle(drawRect, "enable", enableProperty.boolValue);
            // のような記載の仕方だとEditorで複数の同一のComponentを選択すると値が変わるため、EditorGUI.PropertyFieldを使用するように変更

            EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("enable"), new GUIContent("enable"), false);

            drawRect.y += EditorGUIUtility.singleLineHeight + 2f;
            EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("target"), new GUIContent("target"), false);

            drawRect.y += EditorGUIUtility.singleLineHeight + 2f;
            EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("type"), new GUIContent("type"), false);

            var actionTypeProperty = property.FindPropertyRelative("type");
            drawRect.y += EditorGUIUtility.singleLineHeight + 2f;

            switch ((TweenAnimBehaviour.TemporaryCondition.TYPE)actionTypeProperty.enumValueIndex)
            {
                case TweenAnimBehaviour.TemporaryCondition.TYPE.POSITION:
                case TweenAnimBehaviour.TemporaryCondition.TYPE.POSITION_RELATIVE:
                    EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("to_Vec3"), new GUIContent("pos"), false);
                    break;
                case TweenAnimBehaviour.TemporaryCondition.TYPE.SCALE:
                case TweenAnimBehaviour.TemporaryCondition.TYPE.SCALE_RELATIVE:
                    EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("to_Vec3"), new GUIContent("scale"), false);
                    break;
                case TweenAnimBehaviour.TemporaryCondition.TYPE.COLOR:
                    EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("to_Color"), new GUIContent("color"), false);
                    break;
                case TweenAnimBehaviour.TemporaryCondition.TYPE.ALPHA:
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
    //    //using (new EditorGUI.PropertyScope(position, label, property))
    //    {
    //        EditorGUI.BeginProperty(position, label, property);

    //        // 0指定だとReorderableListのドラッグと被るのでLineHeightを指定
    //        position.height = EditorGUIUtility.singleLineHeight;

    //        var drawRect = new Rect(position)
    //        {
    //            y = position.y
    //        };

    //        var enableProperty = property.FindPropertyRelative("enable");
    //        GUI.contentColor = enableProperty.boolValue ? Color.white : Color.grey;
    //        enableProperty.boolValue = EditorGUI.Toggle(drawRect, "enable", enableProperty.boolValue);

    //        drawRect.y += EditorGUIUtility.singleLineHeight + 2f;

    //        var targetProperty = property.FindPropertyRelative("target");
    //        targetProperty.objectReferenceValue = EditorGUI.ObjectField(drawRect, "target", targetProperty.objectReferenceValue, typeof(GameObject), true);

    //        drawRect.y += EditorGUIUtility.singleLineHeight + 2f;

    //        var actionTypeProperty = property.FindPropertyRelative("type");
    //        actionTypeProperty.enumValueIndex = EditorGUI.Popup(drawRect, "type", actionTypeProperty.enumValueIndex, System.Enum.GetNames(typeof(EzTweenBehaviour.TemporaryCondition.TYPE)));

    //        switch ((EzTweenBehaviour.TemporaryCondition.TYPE)actionTypeProperty.enumValueIndex)
    //        {
    //            case EzTweenBehaviour.TemporaryCondition.TYPE.POSITION:
    //            case EzTweenBehaviour.TemporaryCondition.TYPE.POSITION_RELATIVE:
    //                var _property = property.FindPropertyRelative("to_Vec3");
    //                var rect = new Rect(drawRect)
    //                {
    //                    y = drawRect.y + EditorGUIUtility.singleLineHeight + 2f,
    //                };
    //                _property.vector3Value = EditorGUI.Vector3Field(rect, "pos", _property.vector3Value);
    //                break;
    //            case EzTweenBehaviour.TemporaryCondition.TYPE.SCALE:
    //            case EzTweenBehaviour.TemporaryCondition.TYPE.SCALE_RELATIVE:
    //                var _propertyScale = property.FindPropertyRelative("to_Vec3");
    //                var rectScale = new Rect(drawRect)
    //                {
    //                    y = drawRect.y + EditorGUIUtility.singleLineHeight + 2f,
    //                };
    //                _propertyScale.vector3Value = EditorGUI.Vector3Field(rectScale, "scale", _propertyScale.vector3Value);
    //                break;
    //            case EzTweenBehaviour.TemporaryCondition.TYPE.COLOR:
    //                var _propertyColor = property.FindPropertyRelative("to_Color");
    //                var rectColor = new Rect(drawRect)
    //                {
    //                    y = drawRect.y + EditorGUIUtility.singleLineHeight + 2f,
    //                    height = (EditorGUIUtility.singleLineHeight * 1)
    //                };
    //                _propertyColor.colorValue = EditorGUI.ColorField(rectColor, "color", _propertyColor.colorValue);
    //                break;
    //            case EzTweenBehaviour.TemporaryCondition.TYPE.ALPHA:
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

    //        EditorGUI.EndProperty();
    //    }
    //}

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var height = (EditorGUIUtility.singleLineHeight+4) * 4f;

        return height;
    }
}