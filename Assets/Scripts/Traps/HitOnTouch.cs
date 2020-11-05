using Combat;
using UnityEngine;

namespace Traps
{
    [DisallowMultipleComponent]
    public class HitOnTouch : MonoBehaviour
    {
        public int attackDamage = 1;

        public float pushForce = 100.0f;

        /// <summary>
        /// Use this method for Collision event handlers.
        /// </summary>
        /// <param name="thisCollider"></param>
        /// <param name="other"></param>
        public void OnCollision(Collider2D thisCollider, Collision2D other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
            {
                return;
            }

            CombatUnit combatUnit = other.gameObject.GetComponent<CombatUnit>();
            if (combatUnit != null && !combatUnit.IsDead)
            {
                combatUnit.TakeDamage(attackDamage);
                other.rigidbody.AddForce(other.GetContact(0).normal.normalized * pushForce * -1.0f);
            }
        }

        /// <summary>
        /// Use this method for Trigger event handlers.
        /// </summary>
        /// <param name="thisCollider"></param>
        /// <param name="other"></param>
        public void OnTrigger(Collider2D thisCollider, Collider2D other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
            {
                return;
            }

            CombatUnit combatUnit = other.GetComponent<CombatUnit>();
            if (combatUnit != null && !combatUnit.IsDead)
            {
                combatUnit.TakeDamage(attackDamage);
                ContactPoint2D[] contactPoints = new ContactPoint2D[1];
                if (other.GetContacts(contactPoints) > 0)
                {
                    other.attachedRigidbody.AddForce(contactPoints[0].normal.normalized * pushForce * -1.0f);
                }
            }
        }
    }
}
