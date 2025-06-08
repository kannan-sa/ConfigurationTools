using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace ConfigurationTool {
    public abstract class Prompt : MonoBehaviour {

    }

    public abstract class Prompt<T> : Prompt where T : ICloneable {
        public T value;

        public FieldList<T> fieldList;

        public void Add() {
            fieldList.Add((T)value.Clone());
        }
    }
}
