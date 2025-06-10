using UnityEngine;

namespace ConfigurationTool {


    [ExecuteInEditMode]
    public class ListClearer : MonoBehaviour {
        public FieldList[] lists;
        private static ListClearer instance;

        private void Awake() {
            instance = this;
            Clear();
        }

#if UNITY_EDITOR

        [UnityEditor.InitializeOnLoadMethod]
        static void InitializeAfterCompilation() {        //for awake missing after editor compilation..
            if (!instance)
                instance = FindObjectOfType<ListClearer>();
            instance?.Clear();
        }
#endif

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void InitializeAfterEditorSceneLoad() {         //for awake missing on editor entering play mode..
            if (!instance)
                instance = FindObjectOfType<ListClearer>();
            instance?.Clear();
        }

        private void Clear() {
            foreach (var list in lists)
                list.Clear();
        }
    }
}
