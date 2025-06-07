using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace ConfigurationTool {
    [CreateAssetMenu(menuName = "Events/Event")]
    public class Event : ScriptableObject {
        public UnityEvent unityEvent = new UnityEvent();
        public void Register(UnityAction action) {
            unityEvent.AddListener(action);
        }

        public void Unregister(UnityAction action) {
            unityEvent.RemoveListener(action);
        }

        public void Raise() {
            unityEvent.Invoke();
        }
    }

    public abstract class Event<T> : ScriptableObject {

        public UnityEvent<T> unityEvent = new UnityEvent<T>();

        public void Register(UnityAction<T> action) {
            unityEvent.AddListener(action);
        }

        public void Unregister(UnityAction<T> action) {
            unityEvent.RemoveListener(action);
        }

        public void Raise(T value) {
            unityEvent.Invoke(value);
        }
    }
}