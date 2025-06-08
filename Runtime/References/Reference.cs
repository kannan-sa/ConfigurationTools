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
        public int index;

        public T direct;
        public bool useDirect;

        public T value {
            get => useDirect ? direct : list[index];
            set {
                if (useDirect)
                    direct = value;
                else 
                    list[index] = value;
            }
        }

        public static implicit operator T(Reference<T> reference) {
            return reference.value;
        }

        public static implicit operator bool(Reference<T> reference) {
            int index = reference.index;
            int count = reference.list.Count();
            bool inBound = count > index && index > -1;

            bool isReference = !reference.useDirect && reference.list != null && inBound;
            bool isDirect = reference.useDirect && reference.direct != null;
            return isReference || isDirect;
        }
    }

    [Serializable]
    public abstract class ReferenceWithString {

    }

    [Serializable]
    public abstract class ReferenceWithString<T> : ReferenceWithString {
        //linking details
        [SerializeField]
        public FieldList<T> list;
        public string link;

        //direct details
        public bool useDirect;
        public T direct;

        public T value {
            get => list[index];
            set => list[index] = value;
        }

        public int index { get {
                string[] items = list.ToStrings();
                int index = Array.IndexOf(items, link);
                return index;
            }
        } 

        public static implicit operator T(ReferenceWithString<T> reference) {
            return reference ? reference.value : default(T);
        }

        public static implicit operator bool(ReferenceWithString<T> reference) {
            int index = reference.index;
            int count = reference.list.Count();
            bool inBound = count > index && index > -1;

            bool isReference = !reference.useDirect && reference.list != null && inBound;
            bool isDirect = reference.useDirect && reference.direct != null;
            return isReference || isDirect;
        }
    }
}