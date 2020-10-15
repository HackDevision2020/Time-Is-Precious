using System.Collections.Generic;
using Combat.Attribute;
using UnityEngine;

namespace Combat.AttributeSet
{
    [CreateAssetMenu(fileName = "New Attribute Set", menuName = "Combat/Attribute Set", order = 0)]
    public class CombatAttributeSet : ScriptableObject
    {
        public CombatAttribute[] attributes;

        private readonly Dictionary<int, CombatAttribute> idToAttr = new Dictionary<int, CombatAttribute>();

        private void Awake()
        {
            foreach (CombatAttribute attribute in attributes)
            {
                idToAttr.Add(attribute.info.GetInstanceID(), attribute);
            }
        }

        public bool HasAttribute(CombatAttributeInfo attributeInfo)
        {
            return idToAttr.ContainsKey(attributeInfo.GetInstanceID());
        }
    }
}
