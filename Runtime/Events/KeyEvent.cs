using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ConfigurationTool {

    public class KeyEvent : MonoBehaviour {
        public KeyCode keyCode;
        public UnityEvent OnKeyDown;

        void Update() {
            if (Input.GetKeyDown(keyCode)) {
                OnKeyDown.Invoke();
            }
        }
    }
}