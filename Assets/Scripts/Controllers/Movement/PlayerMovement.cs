using Items;
using UnityEngine;

namespace Controllers.Movement
{
    [RequireComponent(typeof(CharacterMovement))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 50.0f;

        public InteractionPoint interactionPoint;

        private CharacterMovement characterMovement;
        private float sideMovementInput;
        private bool jumpInput;

        private Rigidbody2D rigidBody;
        private Animator animator;
        private static readonly int animParamSideSpeed = Animator.StringToHash("SideSpeed");
        private static readonly int animParamUpDownSpeed = Animator.StringToHash("UpDownSpeed");
        private static readonly int animParamOnGround = Animator.StringToHash("OnGround");
        private static readonly int animParamAttack = Animator.StringToHash("Attack");
        private static readonly int animParamHit = Animator.StringToHash("Hit");

        private bool isFacingLeft;
        private bool isAttacking;

        private void Awake()
        {
            characterMovement = GetComponent<CharacterMovement>();
            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

#if UNITY_EDITOR // only executed when the game is run in the editor
            if (interactionPoint == null)
            {
                Debug.LogError($"InteractionPoint not assigned to game object \"{gameObject.name}\"");
            }
#endif
        }

        private void Update()
        {
            if (Time.timeScale == 0.0f) // equality is ok here bc this 0.0f is a value that is directly set
            {
                // do not receive any input when the game is paused
                return;
            }

            if (Input.GetButtonDown("Pause"))
            {
                GameController.Instance.PauseGame(true);
            }

            sideMovementInput = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump"))
            {
                jumpInput = true;
            }

            if (!isAttacking && Input.GetButtonDown("Fire1"))
            {
                characterMovement.PausePhysics();
                isAttacking = true;
                animator.SetTrigger(animParamAttack);
            }

            if (!isAttacking && Input.GetButtonDown("Interact"))
            {
                interactionPoint.Interact();
            }
        }

        private void FixedUpdate()
        {
            if (isAttacking)
            {
                return;
            }

            float movement = sideMovementInput * speed * Time.fixedDeltaTime;
            characterMovement.Move(new Vector2(movement, 0.0f), jumpInput);
            jumpInput = false;

            Vector2 finalVelocity = rigidBody.velocity;

            animator.SetFloat(animParamSideSpeed, Mathf.Abs(finalVelocity.x));
            animator.SetFloat(animParamUpDownSpeed, finalVelocity.y);

            bool wasFacingLeft = isFacingLeft;

            // this prevents changing the direction at the end of the movement
            if (Mathf.Abs(sideMovementInput) > float.Epsilon)
            {
                // change direction
                if (finalVelocity.x < -float.Epsilon)
                {
                    isFacingLeft = true;
                }
                else if (finalVelocity.x > float.Epsilon)
                {
                    isFacingLeft = false;
                }
            }

            if (wasFacingLeft ^ isFacingLeft) // if different
            {
                Vector3 oldScale = transform.localScale;
                transform.localScale = new Vector3(-oldScale.x, oldScale.y, oldScale.z);
            }
        }

        public void SetOnGround(bool isOnGround)
        {
            animator.SetBool(animParamOnGround, isOnGround);
        }

        public void OnAttackEnd()
        {
            characterMovement.ResumePhysics();
            isAttacking = false;
        }

        /// <summary>
        /// Event receiver to trigger animation
        /// </summary>
        /// <param name="current">New value of the health</param>
        /// <param name="previous">Previous value of the health</param>
        public void OnHealthChanged(float current, float previous)
        {
            if (current < previous)
            {
                TakeHit();
            }
        }

        private void TakeHit()
        {
            animator.SetTrigger(animParamHit);
        }

        public void OnDeath()
        {
            animator.SetTrigger("Dead");
        }

        // Platforms logic
        public void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Platform"))
            {
                //Making the player a child
                this.transform.parent = other.transform;
            }
        }
        public void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Platform"))
            {

                this.transform.parent = null;

            }
        }
    }
}
