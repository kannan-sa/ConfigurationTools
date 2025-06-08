using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ConfigurationTool {

    [CustomEditor(typeof(ViewList), true)]    
    public class ViewListInspector : Editor {

        private SerializedProperty constructed;
        private ViewList viewList;

        private void OnEnable() {
            constructed = serializedObject.FindProperty("constructed");
            viewList = (ViewList)target;
        }

        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            string label = constructed.boolValue ? "Destruct" : "Construct";
            bool performOperation = GUILayout.Button(label, EditorStyles.miniButton);
            if (performOperation) {
                if(constructed.boolValue) {
                    viewList.Destruct();
                }
                else {
                    viewList.Construct();
                }

                serializedObject.Update();
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
