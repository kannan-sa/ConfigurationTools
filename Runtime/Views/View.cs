using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ConfigurationTool {

    public abstract class View : MonoBehaviour {

    }

    public abstract class View<T> : View {
        public T model;

        [Header("Events")]
        public Event<T> SelectionEvent;
        public Event<T> UpdateEvent;

        public abstract void Initialize(T instance);

        public virtual void OnEnable() {
            UpdateEvent?.Register(Initialize);
        }

        public virtual void OnDisable() {
            UpdateEvent?.Unregister(Initialize);
        }

        public virtual void Select() {
            SelectionEvent?.Raise(model);
        }

        public virtual void Select(bool active) {
            if(active)
                SelectionEvent?.Raise(model);
        }
    }
}