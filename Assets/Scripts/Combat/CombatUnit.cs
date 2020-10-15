using Combat.Attribute;
using Combat.AttributeSet;
using UnityEngine;

namespace Combat
{
    public class CombatUnit : MonoBehaviour
    {
        public CombatAttributeSet[] attributeSets;

        public bool HasAttribute(CombatAttributeInfo attributeInfo)
        {
            foreach (CombatAttributeSet set in attributeSets)
            {
                if (set.HasAttribute(attributeInfo))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
