using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    public class Interactable : MonoBehaviour
    {
        public bool canBeInteracted = true;

        public SpriteRenderer indicatorSprite;

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
            Color newColor = indicatorSprite.color;
            newColor.a = show && canBeInteracted ? 1.0f : 0.0f;
            indicatorSprite.color = newColor;
        }
    }
}
