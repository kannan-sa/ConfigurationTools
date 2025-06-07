using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

namespace ConfigurationTool {
    [ExecuteInEditMode]
    [CreateAssetMenu(menuName = "List/InputFields", fileName = "InputFields")]
    public class TMPInputListField : FieldList<TMP_InputField> {
        public override string[] ToStrings() {
            return list.Select(t => t.name)
                .ToArray();
        }
    }
}