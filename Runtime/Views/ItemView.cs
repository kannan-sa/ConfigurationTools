using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ConfigurationTool {
    public class ItemView : View<Item> {

        [Header("Texts")]
        public TextMeshProUGUI title;
        public TextMeshProUGUI description;
        public Image icon;

        [Header("Controls")]
        public Toggle toggle;

        public override void Initialize(Item instance) {
            model = instance;
            if(title)
                title.text = instance.title;

            if(description) 
                description.text = instance.description;

            if (icon)
                icon.sprite = instance.icon;

            if(toggle)
                toggle.group = GetComponentInParent<ToggleGroup>();
        }
    }
}