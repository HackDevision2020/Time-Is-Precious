using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Collider2D))]
    [DisallowMultipleComponent]
    public class InteractionPoint : MonoBehaviour
    {
        private readonly List<Interactable> interactables = new List<Interactable>();
        private Interactable currentlySelected;

        public void Interact()
        {
            if (currentlySelected != null)
            {
                currentlySelected.Interact();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Interactable interactable = other.GetComponent<Interactable>();

            if (interactable == null)
            {
                return;
            }

            interactables.Add(interactable);

            if (currentlySelected == null)
            {
                currentlySelected = interactable;
                interactable.ShowInteractable(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Interactable interactable = other.GetComponent<Interactable>();

            if (interactable == null)
            {
                return;
            }

            interactables.Remove(interactable);

            if (interactable != currentlySelected)
            {
                return;
            }

            interactable.ShowInteractable(false);
            currentlySelected = interactables.Count > 0 ? interactables[0] : null;

            if (currentlySelected != null)
            {
                currentlySelected.ShowInteractable(true);
            }
        }
    }
}
