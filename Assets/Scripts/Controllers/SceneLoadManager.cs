using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class SceneLoadManager : MonoBehaviour
    {
        [Range(0.0f, 1.0f)]
        public float loadingDelay = 0.5f;

        public GameObject loadingCanvas;

        public GameObject mainMenuCanvas;

        public string splashSceneName;

        public SceneMode CurrentMode => sceneMode;
        public int CurrentStage => stageNumber;

        private SceneMode sceneMode = SceneMode.MainMenu;
        private int stageNumber = 0;

        private void Awake()
        {
#if UNITY_EDITOR
            if (loadingCanvas == null)
            {
                Debug.LogError("Loading canvas is not assigned to the scene manager!");
            }

            if (mainMenuCanvas == null)
            {
                Debug.LogError("Main menu canvas is not assigned to the scene manager!");
            }
#endif
        }

        private void Start()
        {
#if UNITY_EDITOR
            if (SceneManager.sceneCount == 2)
            {
                mainMenuCanvas.SetActive(false);
                loadingCanvas.SetActive(false);

                if (CheckIfSceneIsLoaded("SplashScreen"))
                {
                    sceneMode = SceneMode.MainScene;
                }
                else
                {
                    foreach (int stageNum in new[] { 1, 2, 3 })
                    {
                        if (CheckIfSceneIsLoaded($"Stage{stageNum}"))
                        {
                            sceneMode = SceneMode.Stage;
                            stageNumber = stageNum;
                            break;
                        }
                    }
                }
            }
#endif

            // This is a fail-safe thing in case someone disables the main menu
            mainMenuCanvas.SetActive(true);
        }

        public void GoToMenu()
        {
            if (sceneMode == SceneMode.MainMenu)
            {
                StartCoroutine(LoadSplashScreen());
            }

            if (sceneMode == SceneMode.MainScene)
            {
                return;
            }

            loadingCanvas.SetActive(true);

            if (SceneManager.sceneCount > 1 && !CheckIfSceneIsLoaded(splashSceneName))
            {
                StartCoroutine(UnloadStage(stageNumber));
            }
        }

        public void StartStage(int stageNum)
        {
            loadingCanvas.SetActive(true);

            if (sceneMode == SceneMode.Stage)
            {
                StartCoroutine(ChangeStage(stageNumber, stageNum));
            }
            else
            {
                mainMenuCanvas.SetActive(false);
                StartCoroutine(LoadStage(stageNum));
            }
        }

        private static bool CheckIfSceneIsLoaded(string sceneName)
        {
            Scene scene = SceneManager.GetSceneByName(sceneName);
            return scene.IsValid() && scene.isLoaded;
        }

        private IEnumerator LoadSplashScreen()
        {
            sceneMode = SceneMode.MainScene;
            stageNumber = 0;

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(splashSceneName, LoadSceneMode.Additive);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            yield return new WaitForSeconds(loadingDelay);
            loadingCanvas.SetActive(false);
        }

        private IEnumerator LoadStage(int stageNum)
        {
            sceneMode = SceneMode.Stage;
            stageNumber = stageNum;

            if (CheckIfSceneIsLoaded(splashSceneName))
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
                AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(splashSceneName);

                while (!asyncUnload.isDone)
                {
                    yield return null;
                }
            }

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync($"Stage{stageNum}", LoadSceneMode.Additive);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            SceneManager.SetActiveScene(SceneManager.GetSceneByName($"Stage{stageNum}"));
            GameController.Instance.PauseGame(false);
            yield return new WaitForSeconds(loadingDelay);
            loadingCanvas.SetActive(false);
        }

        private IEnumerator UnloadStage(int stageNum)
        {
            sceneMode = SceneMode.MainScene;
            stageNumber = 0;
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync($"Stage{stageNum}");

            while (!asyncUnload.isDone)
            {
                yield return null;
            }

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(splashSceneName, LoadSceneMode.Additive);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            GameController.Instance.PauseGame(false);
            yield return new WaitForSeconds(loadingDelay);
            // mainMenuCanvas.SetActive(true);
            loadingCanvas.SetActive(false);
        }

        private IEnumerator ChangeStage(int oldStageNumber, int newStageNumber)
        {
            stageNumber = newStageNumber;
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync($"Stage{oldStageNumber}");

            while (!asyncUnload.isDone)
            {
                yield return null;
            }

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync($"Stage{newStageNumber}", LoadSceneMode.Additive);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            SceneManager.SetActiveScene(SceneManager.GetSceneByName($"Stage{newStageNumber}"));
            GameController.Instance.PauseGame(false);
            yield return new WaitForSeconds(loadingDelay);
            loadingCanvas.SetActive(false);
        }

        public enum SceneMode
        {
            /// <summary>
            /// When the user started the game with the main menu canvas active
            /// </summary>
            MainMenu,

            /// <summary>
            /// When the main scene is open
            /// </summary>
            MainScene,

            /// <summary>
            /// When a stage is open
            /// </summary>
            Stage,
        }
    }
}
