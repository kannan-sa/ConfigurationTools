using System;
using System.Linq;
using UnityEngine;
namespace ConfigurationTool {

    [Serializable]
    public abstract class Reference {

    }

    [Serializable]
    public abstract class Reference<T> : Reference {
        [SerializeField]
        public FieldList<T> list;
        public T value;
        public int index;
        public bool useValue;

        public static implicit operator T(Reference<T> reference) {
            return reference.list.ElementAt(reference.index);
        }

        public static implicit operator bool(Reference<T> reference) {
            return (!reference.useValue && reference.list != null && reference.list.Count() > reference.index) || (reference.value != null && reference.useValue);
        }
    }

    [Serializable]
    public abstract class ReferenceWithString {

    }

    [Serializable]
    public abstract class ReferenceWithString<T> : ReferenceWithString {
        [SerializeField]
        public FieldList<T> list;
        public T value;
        public string link;
        public bool useValue;

        public static implicit operator T(ReferenceWithString<T> reference) {
            string[] items = reference.list.ToStrings();
            int index = Array.IndexOf(items, reference.link);
            return reference.list.ElementAt(index);
        }

        public static implicit operator bool(ReferenceWithString<T> reference) {
            string[] items = reference.list.ToStrings();
            int index = Array.IndexOf(items, reference.link);
            return (!reference.useValue && reference.list != null && reference.list.Count() > index && index > -1) || (reference.value != null && reference.useValue);
        }
    }
}