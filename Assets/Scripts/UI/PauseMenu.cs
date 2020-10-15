using Controllers;
using UnityEngine;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetButtonDown("Pause"))
            {
                GameController.Instance.PauseGame(false);
            }
        }
    }
}
