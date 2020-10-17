using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    public class Interactable : MonoBehaviour
    {
        public bool canBeInteracted = true;

        public GameObject indicator;

        public UnityEvent onInteracted;

        private void Awake()
        {
            if (onInteracted == null)
            {
                onInteracted = new UnityEvent();
            }
        }

        public void Interact()
        {
            onInteracted.Invoke();
        }

        public void ShowInteractable(bool show)
        {
            indicator.SetActive(show && canBeInteracted);
        }
    }
}
