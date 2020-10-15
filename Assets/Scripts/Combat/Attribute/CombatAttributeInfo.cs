using UnityEngine;
using Utils;

namespace Combat.Attribute
{
    [CreateAssetMenu(fileName = "New Combat Attribute Info", menuName = "Combat/Attribute Info", order = 0)]
    public class CombatAttributeInfo : ScriptableObject
    {
        [Uuid]
        public string uuid;

        public string attributeName;

        public string description;
    }
}
