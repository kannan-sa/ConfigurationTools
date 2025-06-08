using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ConfigurationTool {

    [Serializable]
    public class GameObjectReference : ReferenceWithString<GameObject> {
        public void SetActive(bool active) {
            value.SetActive(active);
        }
    }
}