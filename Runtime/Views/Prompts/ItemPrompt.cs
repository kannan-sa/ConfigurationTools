using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace ConfigurationTool {
    public class ItemPrompt : Prompt<Item> {
        [Header("Controls")]
        public TMP_InputField title;
        public TMP_InputField description;

        private void OnEnable() {
            title.onValueChanged.AddListener(OnEditTitle);
            description.onValueChanged.AddListener(OnEditDescription);
        }

        private void OnDisable() {
            title.onValueChanged.RemoveListener(OnEditTitle);
            description.onValueChanged.RemoveListener(OnEditDescription);
        }

        private void OnEditTitle(string arg0) {
            value.title = arg0;
        }

        private void OnEditDescription(string arg0) {
            value.description = arg0;
        }
    }
}
