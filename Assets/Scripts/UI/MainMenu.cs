using Controllers;
using UnityEngine;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public void StartGame()
        {
            GameController.Instance.sceneLoadManager.GoToMenu();
            gameObject.SetActive(false);
        }
    }
}
