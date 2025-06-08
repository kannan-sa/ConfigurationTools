using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ConfigurationTool {
    [CustomPropertyDrawer(typeof(ReferenceWithString), true)]
    public class ReferenceWithStringDrawer : PropertyDrawer {
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
            SerializedProperty link = property.FindPropertyRelative("link");
            SerializedProperty direct = property.FindPropertyRelative("direct");
            SerializedProperty useDirect = property.FindPropertyRelative("useDirect");

            // Calculate rect for configuration button
            Rect buttonRect = new Rect(position);
            buttonRect.yMin += popupStyle.margin.top;
            buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
            position.xMin = buttonRect.xMax;

            // Store old indent level and set it to 0, the PrefixLabel takes care of it
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            popupOptions[1] = useDirect.boolValue ? "Use Reference" : "Use Direct";

            int result = EditorGUI.Popup(buttonRect, -1, popupOptions, popupStyle);

            switch (result) {
                case 0:
                    list.objectReferenceValue = null;
                    break;
                case 1:
                    useDirect.boolValue = !useDirect.boolValue;
                    break;
            }

            if (useDirect.boolValue) {
                EditorGUI.PropertyField(position,
                            direct,
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
                    string[] options = fieldList.ToStrings();
                    int index = Array.IndexOf(options, link.stringValue);
                    index = EditorGUI.Popup(position, index, options);

                    if (index > -1)
                        link.stringValue = options[index];
                }
            }
            EditorGUI.indentLevel = indent;

            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();
            EditorGUI.EndProperty();
        }
    }
}
