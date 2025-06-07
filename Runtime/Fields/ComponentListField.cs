using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ConfigurationTool {
    [ExecuteInEditMode]
    [CreateAssetMenu(menuName = "List/Components", fileName = "Components")]
    public class ComponentListField : FieldList<Component> {
        public override string[] ToStrings() {
            return list.Select(t => t.name)
                .ToArray();
        }
    }
}