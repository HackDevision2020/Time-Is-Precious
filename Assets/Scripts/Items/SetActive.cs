using UnityEngine;

namespace Items
{
    public class SetActive : MonoBehaviour
    {
        public GameObject target;

        public SetActiveMode mode;

        public void OnInteraction()
        {
            switch (mode)
            {
                case SetActiveMode.Toggle:
                    target.SetActive(!target.activeSelf);
                    break;

                case SetActiveMode.Activate:
                    target.SetActive(true);
                    break;

                case SetActiveMode.Deactivate:
                    target.SetActive(false);
                    break;
            }
        }

        public enum SetActiveMode
        {
            Toggle,
            Activate,
            Deactivate,
        }
    }
}
