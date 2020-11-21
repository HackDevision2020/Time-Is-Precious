using UnityEngine;

namespace Combat
{
    [DisallowMultipleComponent]
    public class MeleeAttackCollider : MonoBehaviour
    {
        [Tooltip("Layers of things that can be hit")]
        public LayerMask targetLayer;

        public int attackDamage = 0;

        public float pushForce = 100.0f;

        public void OnTargetHit(Collider2D thisCollider, Collider2D other)
        {
            if ((1 << other.gameObject.layer & targetLayer) == 0)
            {
                return;
            }

            skeletonManager skeleton = other.GetComponent<skeletonManager>();
            if (skeleton)
            {
                Destroy(skeleton.gameObject);
                return;
            }

            ContactPoint2D[] contactPoints = new ContactPoint2D[1];
            if (other.GetContacts(contactPoints) > 0)
            {
                Vector3 currentPosition = transform.position;
                Vector2 pushDirection = contactPoints[0].point - new Vector2(currentPosition.x, currentPosition.y);
                other.attachedRigidbody.AddForce(pushDirection.normalized * pushForce * 1.0f);
            }
        }
    }
}
