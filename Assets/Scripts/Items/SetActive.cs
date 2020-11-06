using UnityEngine;

namespace Items
{
    public class SetActive : MonoBehaviour
    {
        public GameObject[] target;

        public SetActiveMode mode;

        public void OnInteraction()
        {
            switch (mode)
            {
                case SetActiveMode.Toggle:
                    foreach (GameObject t in target)
                    {
                        t.SetActive(!t.activeSelf);
                    }
                    break;

                case SetActiveMode.Activate:
                    foreach (GameObject t in target)
                    {
                        t.SetActive(true);
                    }
                    break;

                case SetActiveMode.Deactivate:
                    foreach (GameObject t in target)
                    {
                        t.SetActive(false);
                    }
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
