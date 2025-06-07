using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace ConfigurationTool {
    [CustomPropertyDrawer(typeof(Reference), true)]
    public class ReferenceDrawer : PropertyDrawer {
        private readonly string[] popupOptions =
               { "Reset", "Use Variable" };

        /// <summary> Cached style to use to draw the popup button. </summary>
        private GUIStyle popupStyle;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            if (popupStyle == null) {
                popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
                popupStyle.imagePosition = ImagePosition.ImageOnly;
            }

            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            EditorGUI.BeginChangeCheck();

            // Get properties
            SerializedProperty list = property.FindPropertyRelative("list");
            SerializedProperty index = property.FindPropertyRelative("index");
            SerializedProperty value = property.FindPropertyRelative("value");
            SerializedProperty useValue = property.FindPropertyRelative("useValue");

            // Calculate rect for configuration button
            Rect buttonRect = new Rect(position);
            buttonRect.yMin += popupStyle.margin.top;
            buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
            position.xMin = buttonRect.xMax;

            // Store old indent level and set it to 0, the PrefixLabel takes care of it
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            popupOptions[1] = useValue.boolValue ? "Use Reference" : "Use Value";

            int result = EditorGUI.Popup(buttonRect, -1, popupOptions, popupStyle);

            switch (result) {
                case 0:
                    list.objectReferenceValue = null;
                    break;
                case 1:
                    useValue.boolValue = !useValue.boolValue;
                    break;
            }

            if (useValue.boolValue) {
                EditorGUI.PropertyField(position,
                            value,
                            GUIContent.none);
            }
            else {

                if (list.objectReferenceValue == null) {
                    EditorGUI.PropertyField(position,
                            list,
                            GUIContent.none);
                }
                else {
                    FieldList fieldList = list.objectReferenceValue as FieldList;
                    index.intValue = EditorGUI.Popup(position, index.intValue, fieldList.ToStrings());
                }
            }
            EditorGUI.indentLevel = indent;

            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();
            EditorGUI.EndProperty();
        }
    }
}