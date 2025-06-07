using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConfigurationTool {

    [Serializable]
    public partial class Item {
        public string title;
        public string description;
        public string iconPath;
        public Sprite icon;
    }

    [CreateAssetMenu(menuName = "List/Items", fileName = "Items")]

    public class ItemList : FieldList<Item> {
        public override string GetTitle(int index) {
            return list[index].title;
        }

        public override bool IsPictureMode(out string spriteProperty) {
            spriteProperty = "icon";
            return true;
        }
    }
}