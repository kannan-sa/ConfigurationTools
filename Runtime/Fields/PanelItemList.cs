using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConfigurationTool {

    [Serializable]
    public class PanelItem : Item {
        public GameObjectReference panel;
    }

    [CreateAssetMenu(menuName = "List/PanelItems", fileName = "PanelItems")]

    public class PanelItemList : FieldList<PanelItem> {
        public override string GetTitle(int index) {
            return list[index].title;
        }

        public override bool IsPictureMode(out string spriteProperty) {
            spriteProperty = "icon";
            return true;
        }
    }
}