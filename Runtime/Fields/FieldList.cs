using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ConfigurationTool {
    public abstract class FieldList : ScriptableObject {
        public abstract void Clear();
        public abstract string GetTitle(int index);
        public abstract string GetCategory(int index);  
        public abstract bool IsPictureMode(out string spriteProperty);
        public abstract string[] ToStrings();
    }

    public class FieldList<T> : FieldList, IEnumerable<T> {
        public List<T> list = new List<T>();

        //Delegates
        public Action<T> Added;
        public Action<T> Removed;

        public IEnumerator<T> GetEnumerator() {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public T this [int index] {
            get { return list[index]; }
            set { list[index] = value; }
        }

        public override void Clear() { 
            list.Clear(); 
        }

        public void Add(T item) { 
            list.Add(item);  
            Added?.Invoke(item);
        }

        public void Remove(T item) {
            list.Remove(item);
            Removed?.Invoke(item);
        }

        public bool Contains(T item) {
            return list.Contains(item);
        }
        
        public override string GetTitle(int index) {
            return list[index].ToString();
        }

        public override string GetCategory(int index) {
            return string.Empty;
        }

        public override bool IsPictureMode(out string spriteProperty) {
            spriteProperty = string.Empty;
            return false;
        }

        public override string[] ToStrings() {
            string[] results = list.Select(i => i.ToString())
                .ToArray();

            return results;
        }

        internal void SyncItems() {
            throw new NotImplementedException();
        }
    }
}