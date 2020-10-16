using Combat;
using UnityEngine;

namespace Traps
{
    [DisallowMultipleComponent]
    public class HitOnTouch : MonoBehaviour
    {
        public float attackDamage = 1.0f;

        public float pushForce = 100.0f;

        public void OnCollision(Collider2D thisCollider, Collision2D other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
            {
                return;
            }

            CombatUnit combatUnit = other.gameObject.GetComponent<CombatUnit>();
            if (combatUnit != null)
            {
                combatUnit.TakeDamage(attackDamage);
                other.rigidbody.AddForce(other.GetContact(0).normal.normalized * pushForce * -1.0f);
            }
        }
    }
}
