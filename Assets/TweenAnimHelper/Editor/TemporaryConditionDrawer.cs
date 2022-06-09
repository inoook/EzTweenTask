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
[CustomPropertyDrawer(typeof(TemporaryCondition),true)]
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
            switch ((TemporaryCondition.TYPE)actionTypeProperty.enumValueIndex)
            {
                case TemporaryCondition.TYPE.POSITION:
                case TemporaryCondition.TYPE.POSITION_RELATIVE:
                    EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("to_Vec3"), new GUIContent("pos"), false);
                    break;
                case TemporaryCondition.TYPE.SCALE:
                case TemporaryCondition.TYPE.SCALE_RELATIVE:
                    EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("to_Vec3"), new GUIContent("scale"), false);
                    break;
                case TemporaryCondition.TYPE.COLOR:
                    EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("to_Color"), new GUIContent("color"), false);
                    break;
                case TemporaryCondition.TYPE.ALPHA:
                    EditorGUI.PropertyField(drawRect, property.FindPropertyRelative("to_V"), new GUIContent("alpha"), false);
                    break;

                default: break;
            }

            GUI.contentColor = Color.white;

            EditorGUI.EndProperty();
        }
    }


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var height = (EditorGUIUtility.singleLineHeight+4) * 4f;

        return height;
    }
}