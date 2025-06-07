using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ConfigurationTool {
    [ExecuteInEditMode]
    [CreateAssetMenu(menuName = "List/Transforms", fileName = "Transforms")]
    public class TransformListField : FieldList<Transform> {
        public override string[] ToStrings() {
            return list.Select(t => t.name)
                .ToArray();
        }
    }
}