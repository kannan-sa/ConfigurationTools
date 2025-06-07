using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ConfigurationTool {
    public abstract class FieldList : ScriptableObject {
        public abstract string[] ToStrings();
        public abstract void Clear();
    }

    public class FieldList<T> : FieldList, IEnumerable<T> {
        public List<T> list = new List<T>();

        public IEnumerator<T> GetEnumerator() {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public override void Clear() { list.Clear(); }
        public void Add(T item) { list.Add(item); }
        public bool Contains(T item) { return list.Contains(item); }

        public override string[] ToStrings() {
            string[] results = list.Select(i => i.ToString())
                .ToArray();

            return results;
        }
    }
}