using UnityEngine;

namespace ConfigurationTool {

    [ExecuteInEditMode]
    public abstract class ListInitializer : MonoBehaviour {

    }

    [ExecuteInEditMode]
    public abstract class ListInitializer<T> : ListInitializer {
        public bool log;
        public bool clearOnInitialize;
        [SerializeField]
        public FieldList<T> list;

        public T[] values;


        protected void OnEnable() {
            if (clearOnInitialize)
                list.Clear();

            if (log)
                Debug.Log(name + " OnEnable");

            list.list.AddRange(values);
        }
    }
}