using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ConfigurationTool {

    public abstract class ViewList : MonoBehaviour {
        public abstract void Construct();
        public abstract void Destruct();
    }

    public abstract class ViewList<T> : ViewList {

        public bool constructOnStart;
        public bool constructed;
        public Transform content;
        public FieldList<T> list;
        public View<T> view;

        //[HideInInspector]
        public List<View<T>> views = new List<View<T>>();

        public virtual void OnEnable() {
           if(constructed) {
                list.Added += Construct;
                list.Removed += Destruct;
                SyncItems();
            }
        }

        private void SyncItems() {
            int count = Mathf.Max(views.Count, list.Count());
            for (int i = 0; i < count; i++) {
                bool viewAvailable = i < views.Count;
                bool itemAvailable = i < list.Count();
                if (viewAvailable && itemAvailable)
                    views[i].Initialize(list[i]);   //initialize if views are available..
                else if (!viewAvailable && itemAvailable)
                    Construct(list[i]); //construct if views are over..
                else if (viewAvailable && !itemAvailable)
                    Destruct(views[i]); //Destruct if items are over..
            }
        }

        public virtual void OnDisable() {
            if (constructed) {
                list.Added -= Construct;
                list.Removed -= Destruct;
            }
        }

        public virtual void Start() {
            if (constructOnStart && !constructed) {
                Construct();
            }
        }

        public override void Construct() {
            foreach (var item in list) {
                if (CanvasUpdateRegistry.IsRebuildingLayout() || CanvasUpdateRegistry.IsRebuildingGraphics()) {
                    StartCoroutine(DelayAction(() => Construct(item)));
                }
                else
                    Construct(item);
            }

            list.Added += Construct;
            list.Removed += Destruct;
            constructed = true;
        }

        public override void Destruct() {
            content.ClearChildren();
            views.Clear();
            list.Added -= Construct;
            list.Removed -= Destruct;
            constructed = false;
        }

        private void Construct(T item) {
#if UNITY_EDITOR
            View<T> newView = (View<T>)UnityEditor.PrefabUtility.InstantiatePrefab(view, content);
#else
            View<T> newView = Instantiate(view, content, false);
#endif
            newView.name = list.GetTitle(list.list.IndexOf(item));
            newView.Initialize(item);
            views.Add(newView);
        }

        private void Destruct(T item) {
            View<T> view = views.FirstOrDefault(x => x.model.Equals(item));
            if (view != null)
                Destruct(view);
        }

        private void Destruct(View<T> view)
        {
            if (Application.isPlaying)
                Destroy(view.gameObject);
            else
                DestroyImmediate(view.gameObject);

            views.Remove(view);
        }

        IEnumerator DelayAction(Action action) {
            yield return null; // Wait for next frame
            action?.Invoke();
        }
    }
}
