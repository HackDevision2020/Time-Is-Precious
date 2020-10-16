using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    [RequireComponent(typeof(Collider2D))]
    public class ColliderEvents : MonoBehaviour
    {
        [System.Serializable]
        public class Trigger2DEvent : UnityEvent<Collider2D, Collider2D> {}

        [System.Serializable]
        public class Collision2DEvent : UnityEvent<Collider2D, Collision2D> {}

        public Trigger2DEvent onTriggerEnter;
        public Trigger2DEvent onTriggerExit;
        public Trigger2DEvent onTriggerStay;

        public Collision2DEvent onCollisionEnter;
        public Collision2DEvent onCollisionExit;
        public Collision2DEvent onCollisionStay;

        private new Collider2D collider;

        private void Awake()
        {
            collider = GetComponent<Collider2D>();

            if (onTriggerEnter == null)
            {
                onTriggerEnter = new Trigger2DEvent();
            }

            if (onTriggerExit == null)
            {
                onTriggerExit = new Trigger2DEvent();
            }

            if (onTriggerStay == null)
            {
                onTriggerStay = new Trigger2DEvent();
            }

            if (onCollisionEnter == null)
            {
                onCollisionEnter = new Collision2DEvent();
            }

            if (onCollisionExit == null)
            {
                onCollisionExit = new Collision2DEvent();
            }

            if (onCollisionStay == null)
            {
                onCollisionStay = new Collision2DEvent();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            onTriggerEnter.Invoke(collider, other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            onTriggerExit.Invoke(collider, other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            onTriggerStay.Invoke(collider, other);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            onCollisionEnter.Invoke(collider, other);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            onCollisionExit.Invoke(collider, other);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            onCollisionStay.Invoke(collider, other);
        }
    }
}
