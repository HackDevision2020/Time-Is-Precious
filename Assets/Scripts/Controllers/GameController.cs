using UnityEngine;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance => instance;

        private static GameController instance;

        public SceneLoadManager sceneLoadManager;

        public GameStateTracker gameStateTracker;

        public GameObject pauseMenuCanvas;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("There are two instances of game controller!!!");
            }

            instance = this;

            pauseMenuCanvas.SetActive(false);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void PauseGame(bool pause)
        {
            pauseMenuCanvas.SetActive(pause);
            Time.timeScale = pause ? 0.0f : 1.0f;
        }
    }
}
