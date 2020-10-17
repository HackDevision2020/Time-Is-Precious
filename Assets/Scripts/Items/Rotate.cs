using UnityEngine;

namespace Items
{
    public class Rotate : MonoBehaviour
    {
        [Min(0.01f)]
        public float rotationAngle = 90.0f;

        public bool startNegative;

        public bool toggleDirection;

        private float nextAngle;

        private void Awake()
        {
            nextAngle = startNegative ? -rotationAngle : rotationAngle;
        }

        public void OnInteraction()
        {
            transform.localRotation *= Quaternion.Euler(0.0f, 0.0f, nextAngle);

            if (toggleDirection)
            {
                nextAngle = nextAngle > 0 ? -rotationAngle : rotationAngle;
            }
        }
    }
}
