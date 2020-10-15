using UnityEngine;

namespace Controllers
{
    [DisallowMultipleComponent]
    public class CameraMotionController : MonoBehaviour
    {
        /// <summary>
        /// Transform component of the target to follow
        ///
        /// This is meant to be set outside of this class.
        /// </summary>
        [HideInInspector]
        public Transform followTarget;

        /// <summary>
        /// Whether the position should update on "FixedUpdate" or "Update"
        ///
        /// This is important because some strange motion might occur if the target is updated in "FixedUpdate" but the
        /// camera is updated in "Update" and vice versa.
        /// </summary>
        [Header("Follow")]
        [Tooltip("Whether the position of the target is updated with \"FixedUpdate\" or not")]
        public bool updateOnPhysics;

        [Tooltip("Time in seconds it take for the camera to catch up with the target")]
        [Range(0.01f, 1.0f)]
        public float followSmoothTime = 0.3f;

        [Min(0.1f)]
        public float followMaxSpeed = 10000.0f;

        [Tooltip("Where the camera should be relative to the target when the camera finally caught up with the target")]
        public Vector3 followOffset = new Vector3(0.0f, 0.0f, -10.0f);

        private Vector3 targetPosition = Vector3.zero;
        private Vector3 currentVelocity = Vector3.zero;

        private void Update()
        {
            if (updateOnPhysics)
            {
                // Position should be updated in FixedUpdate
                return;
            }

            SetPosition();
        }

        private void FixedUpdate()
        {
            if (!updateOnPhysics)
            {
                // Position should be updated in Update
                return;
            }

            SetPosition();
        }

        /// <summary>
        /// Update the position of the object
        /// </summary>
        private void SetPosition()
        {
            if (followTarget != null)
            {
                // Update the position only when the target object is not destroyed
                targetPosition = followTarget.position;
            }

            Vector3 destination = targetPosition + followOffset;
            float distance = (destination - transform.position).magnitude;

            if (distance > float.Epsilon) // same as "> 0" but more safe for floating point numbers
            {
                transform.position = Vector3.SmoothDamp(
                    transform.position,
                    destination,
                    ref currentVelocity,
                    followSmoothTime,
                    followMaxSpeed);
            }
        }
    }
}
