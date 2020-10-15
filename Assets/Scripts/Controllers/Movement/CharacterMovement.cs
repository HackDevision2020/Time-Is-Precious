using UnityEngine;
using UnityEngine.Events;

namespace Controllers.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMovement : MonoBehaviour
    {
        [Min(1.0f)]
        public float jumpForce = 500.0f;

        [Min(0.001f)]
        public float smoothness = 0.05f;

        [Range(0.0f, 1.0f)]
        public float airControlFactor = 0.2f;

        [Header("Ground Check")]
        public LayerMask groundLayer;

        public Transform groundCheckPosition;

        public float groundCheckRadius = 0.1f;

        [Header("Events")]
        public UnityEvent onLanded;

        public UnityEvent onOffGround;

        private Rigidbody2D rigidBody;
        private Vector3 acceleration;
        private bool isOnGround;

        private Vector2 previousVelocity = Vector2.zero;
        private float previousGravityScale = 1.0f;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
#if UNITY_EDITOR // only executed when the game is run in the editor
            if (groundCheckPosition == null)
            {
                Debug.LogError($"groundCheckPosition not assigned for object \"{gameObject.name}\"");
                return;
            }
#endif
            bool wasOnGround = isOnGround;
            isOnGround = Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundLayer);

            // events invocations
            if (wasOnGround && !isOnGround)
            {
                onOffGround.Invoke();
            }
            else if (!wasOnGround && isOnGround)
            {
                onLanded.Invoke();
            }
        }

        public void Move(Vector2 movement, bool tryJump)
        {
            float targetSideVelocity = movement.x;
            Vector2 rigidBodyVelocity = rigidBody.velocity;

            if (!isOnGround)
            {
                targetSideVelocity = Mathf.Lerp(rigidBodyVelocity.x, targetSideVelocity, airControlFactor);
            }

            Vector3 targetVelocity = new Vector2(targetSideVelocity, rigidBodyVelocity.y);
            rigidBody.velocity = Vector3.SmoothDamp(
                rigidBodyVelocity,
                targetVelocity,
                ref acceleration,
                smoothness);

            if (isOnGround && tryJump)
            {
                isOnGround = false;
                rigidBody.AddForce(new Vector2(0.0f, jumpForce));
            }
        }

        public void PausePhysics()
        {
            previousVelocity = rigidBody.velocity;
            previousGravityScale = rigidBody.gravityScale;

            rigidBody.bodyType = RigidbodyType2D.Kinematic;
            rigidBody.velocity = Vector2.zero;
            rigidBody.gravityScale = 0.0f;
        }

        public void ResumePhysics()
        {
            rigidBody.gravityScale = previousGravityScale;
            rigidBody.velocity = previousVelocity;
            rigidBody.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
