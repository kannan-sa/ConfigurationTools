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
        private void OnEnable() {
            labelText = target.name.Length > 0 ? target.name.Substring(0, target.name.Length - 1) : string.Empty;
            listProperty = serializedObject.FindProperty("list");
            list = new ReorderableList(serializedObject, listProperty);
            list.drawHeaderCallback = (l) => EditorGUI.LabelField(l, target.name);
            list.drawElementCallback = DrawDVPProperty;
        }

        private void DrawDVPProperty(Rect rect, int index, bool isActive, bool isFocused) {
            SerializedProperty item = listProperty.GetArrayElementAtIndex(index);

            GUIContent label = GUIContent.none;
            if (UnityEngine.Event.current.type == EventType.Repaint && rect.width > extendedWidth)
                label = new GUIContent($"{labelText} {index}");

            switch(item.propertyType) {
                case SerializedPropertyType.ObjectReference:
                    Type type = item.objectReferenceValue ? item.objectReferenceValue.GetType() : typeof(object);
                    EditorGUI.ObjectField(rect, label, item.objectReferenceValue, type, true);
                    break;
                default:
                    EditorGUI.PropertyField(rect, item, label);
                    break;
            }
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();
            list.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
