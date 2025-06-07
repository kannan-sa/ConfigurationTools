using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ConfigurationTool {

    public static class SerializedPropertyExtension {

        public static IEnumerable<SerializedProperty> GetChildren(this SerializedProperty serializedProperty) {
            SerializedProperty currentProperty = serializedProperty.Copy();
            SerializedProperty nextSiblingProperty = serializedProperty.Copy();
            {
                nextSiblingProperty.Next(false);
            }

            if (currentProperty.Next(true)) {
                do {
                    if (SerializedProperty.EqualContents(currentProperty, nextSiblingProperty))
                        break;

                    yield return currentProperty;
                }
                while (currentProperty.Next(false));
            }
        }

        public static void Render(this SerializedProperty property, Rect position, GUIContent label, params string[] avoidProperties) {

            position.height = EditorGUIUtility.singleLineHeight;

            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label);

            if (!property.isExpanded)
                return;
            float intendWidth = 5;
            position.x += intendWidth;
            position.width -= intendWidth;
            foreach (var child in property.GetChildren()) {

                if (avoidProperties.Contains(child.name))
                    continue;
                position.y += position.height;
                position.height = EditorGUI.GetPropertyHeight(child);
                EditorGUI.BeginChangeCheck();
                EditorGUI.PropertyField(position, child, includeChildren: true);
                if (EditorGUI.EndChangeCheck()) {
                    child.serializedObject.ApplyModifiedProperties();
                }
            }
        }

        public static void RenderPictureMode(this SerializedProperty property, Rect position, GUIContent label, string pictureProperty, params string[] avoidProperties) {

            float fieldCount = position.height / EditorGUIUtility.singleLineHeight;
            bool isExtending = fieldCount >= 4;
            Rect rect = position;
            float imageWidth = 60;
            float oldLabelWidth = EditorGUIUtility.labelWidth;
            float newLabelWidth = oldLabelWidth - imageWidth;

            if (isExtending)
                rect.y += 8;

            rect.height = EditorGUIUtility.singleLineHeight * 4;
            rect.width = imageWidth;

            SerializedProperty spriteProperty = property.FindPropertyRelative(pictureProperty);
            spriteProperty.objectReferenceValue = EditorGUI.ObjectField(rect, GUIContent.none, spriteProperty.objectReferenceValue, typeof(Sprite), false);

            if(GUI.changed)
                property.serializedObject.ApplyModifiedProperties();

            rect.height = EditorGUIUtility.singleLineHeight;

            rect.y += isExtending ? -4 : 4;
            int i = 0;
            foreach (var child in property.GetChildren()) {
                if (avoidProperties.Contains(child.name))
                    continue;

                if (i < 4) {
                    rect.x = position.x + imageWidth + 5;
                    rect.width = position.width - imageWidth - 5;
                    EditorGUIUtility.labelWidth = newLabelWidth - 5;
                }
                else {
                    EditorGUIUtility.labelWidth = oldLabelWidth;
                    rect.x = position.x;
                    rect.width = position.width;
                }

                rect.height = EditorGUI.GetPropertyHeight(child);
                EditorGUI.BeginChangeCheck();
                EditorGUI.PropertyField(rect, child, includeChildren: true);
                if (EditorGUI.EndChangeCheck()) {
                    child.serializedObject.ApplyModifiedProperties();
                }
                rect.y += EditorGUIUtility.singleLineHeight;
                i++;
            }
            EditorGUIUtility.labelWidth = oldLabelWidth;
        }


        public static float GetHeight(this SerializedProperty property, params string[] avoidProperties) {
            float height = 0f;
            foreach (var child in property.GetChildren()) {

                if (avoidProperties.Contains(child.name))
                    continue;

                height += EditorGUI.GetPropertyHeight(child);
            }
            return height;
        }
    }
}