using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ConfigurationTool {
    [ExecuteInEditMode]
    [CreateAssetMenu(menuName = "List/Images", fileName = "Images")]
    public class ImageListField : FieldList<Image> {
        public override string[] ToStrings() {
            return list.Select(t => t.name)
                .ToArray();
        }
    }
}