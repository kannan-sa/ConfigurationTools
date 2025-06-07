using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

namespace ConfigurationTool {
    [ExecuteInEditMode]
    [CreateAssetMenu(menuName = "List/Texts", fileName = "Texts")]
    public class TMPTextListField : FieldList<TextMeshProUGUI> {
        public override string[] ToStrings() {
            return list.Select(t => t.name)
                .ToArray();
        }
    }
}