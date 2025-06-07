using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ConfigurationTool {
    [ExecuteInEditMode]
    public class AwakeEvent : MonoBehaviour {
        public UnityEvent OnEvent = new UnityEvent();

        private static AwakeEvent instance;

        private void Awake() {
            instance = this;
            OnEvent.Invoke();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnRuntimeMethodLoad() {
            if (instance)
                instance.OnEvent.Invoke();
        }
    }

}