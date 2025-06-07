using UnityEngine;

using UnityEditor;
using UnityEditorInternal;
using System;

namespace ConfigurationTool {
    [CustomEditor(typeof(FieldList), true)]
    public class FieldListInspector : Editor {
        private ReorderableList list;
        private SerializedProperty listProperty;

        private int extendedWidth = 300;
        private string labelText;
        private FieldList fieldList;

        private bool pictureRenderMode;
        private string pictureProperty = string.Empty;

        private void OnEnable() {

            fieldList = (FieldList)target;
            pictureRenderMode = fieldList.IsPictureMode(out pictureProperty);

            labelText = target.name.Length > 0 ? target.name.Substring(0, target.name.Length - 1) : string.Empty;
            listProperty = serializedObject.FindProperty("list");
            list = new ReorderableList(serializedObject, listProperty);
            list.drawHeaderCallback = (l) => EditorGUI.LabelField(l, target.name);
            list.drawElementCallback = DrawElement;
            list.elementHeightCallback = GetElementHeight;
        }

        private void DrawElement(Rect rect, int index, bool isActive, bool isFocused) {
            SerializedProperty element = listProperty.GetArrayElementAtIndex(index);

            GUIContent label = GUIContent.none;

            if (UnityEngine.Event.current.type == EventType.Repaint && rect.width > extendedWidth)
                label = new GUIContent($"{labelText} {index}");

            switch(element.propertyType) {
                case SerializedPropertyType.Generic:
                    if (pictureRenderMode) {
                        element.RenderPictureMode(rect, label, pictureProperty, pictureProperty);
                    }
                    else {
                        string title = fieldList.GetTitle(index);
                        if (!string.IsNullOrEmpty(title))
                            label = new GUIContent(title);
                        element.Render(rect, label);
                    }
                    break;
                case SerializedPropertyType.ObjectReference:
                    Type type = element.objectReferenceValue ? element.objectReferenceValue.GetType() : typeof(object);
                    EditorGUI.ObjectField(rect, label, element.objectReferenceValue, type, true);
                    break;
                default:
                    EditorGUI.PropertyField(rect, element, label);
                    break;
            }
        }

        private float GetElementHeight(int index) {
            float height = EditorGUIUtility.singleLineHeight;
            SerializedProperty element = listProperty.GetArrayElementAtIndex(index);
            float elementHeight = element.GetHeight();
            switch (element.propertyType) {
                case SerializedPropertyType.Generic:
                    if (pictureRenderMode) {
                        float fieldCount = elementHeight / EditorGUIUtility.singleLineHeight;
                        int imageHeights = 4;
                        if (fieldCount < imageHeights)
                            fieldCount = imageHeights;
                        else
                            fieldCount--; //removing sprite property space..
                            height = fieldCount * EditorGUIUtility.singleLineHeight;

                        if (fieldCount < imageHeights)
                            height += 5;
                        else
                            height += 3;
                    }
                    else {
                        if (element.isExpanded)
                            height += elementHeight;
                    }
                    break;
            }
            return height;
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();
            list.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
