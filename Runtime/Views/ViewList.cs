using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConfigurationTool {

    public abstract class ViewList : MonoBehaviour {

    }

    public abstract class ViewList<T> : ViewList {

        public bool populateOnStart;

        public FieldList<T> list;
        public View<T> view;

        private List<View<T>> views = new List<View<T>>();

        public virtual void Start() {
            if (populateOnStart) {
                Populate();
            }
        }

        public void Populate() {

            foreach (var item in list) {
                View<T> newView = Instantiate(view, transform, false);
                newView.Initialize(item);
                views.Add(newView);
            }
        }
    }
}
