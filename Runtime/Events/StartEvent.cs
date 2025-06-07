using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ConfigurationTool {

    [ExecuteInEditMode]
    public class StartEvent : MonoBehaviour {
        public UnityEvent OnEvent = new UnityEvent();

        private void Start() {
            OnEvent.Invoke();
        }
    }
}