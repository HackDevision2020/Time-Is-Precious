using UnityEngine;

namespace Utils
{
    public class InstantFollowCamera : MonoBehaviour
    {
        public bool updateOnPhysics;

        public bool followZ;

        public Vector3 offset = Vector3.zero;

        private void Update()
        {
            if (!updateOnPhysics)
            {
                UpdatePosition();
            }
        }

        private void FixedUpdate()
        {
            if (updateOnPhysics)
            {
                UpdatePosition();
            }
        }

        private void UpdatePosition()
        {
            Camera mainCamera = Camera.main;

            if (mainCamera == null)
            {
                return;
            }

            Vector3 destination = mainCamera.transform.position + offset;
            if (!followZ)
            {
                destination.z = transform.position.z;
            }

            transform.position = destination;
        }
    }
}
