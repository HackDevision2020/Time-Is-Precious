using UnityEngine;

namespace Combat
{
    [DisallowMultipleComponent]
    public class MeleeAttackCollider : MonoBehaviour
    {
        [Tooltip("Layers of things that can be hit")]
        public LayerMask targetLayer;

        public int attackDamage = 1;

        public float pushForce = 100.0f;

        private ContactPoint2D[] contactPoints;

        private void Awake()
        {
            contactPoints = new ContactPoint2D[1];
        }

        public void OnTargetHit(Collider2D thisCollider, Collider2D other)
        {
            if ((1 << other.gameObject.layer & targetLayer) == 0)
            {
                return;
            }

            CombatUnit combatUnit = other.GetComponent<CombatUnit>();
            if (combatUnit)
            {
                combatUnit.TakeDamage(1);
            }

            bool isGhost = other.gameObject.layer == LayerMask.NameToLayer("Ghost");
            if (isGhost || other.GetContacts(contactPoints) > 0)
            {
                Vector3 currentPosition = transform.position;
                Vector2 targetPosition = isGhost
                    ? other.attachedRigidbody.position
                    : contactPoints[0].point;
                Vector2 pushDirection = targetPosition - new Vector2(currentPosition.x, currentPosition.y);
                other.attachedRigidbody.AddForce(pushDirection.normalized * pushForce * 1.0f);
            }
        }
    }
}
