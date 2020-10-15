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

        private int sceneMode = 0;

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

                foreach (int stageNum in new[] { 1, 2, 3 })
                {
                    Scene stage = SceneManager.GetSceneByBuildIndex(stageNum);
                    if (stage.IsValid() && stage.isLoaded)
                    {
                        sceneMode = stageNum;
                        break;
                    }
                }
            }
#endif

            if (sceneMode == 0)
            {
                mainMenuCanvas.SetActive(true);
            }
        }

        public void GoToMenu()
        {
            if (sceneMode == 0)
            {
                return;
            }

            loadingCanvas.SetActive(true);

            if (SceneManager.sceneCount > 1)
            {
                StartCoroutine(UnloadStage(sceneMode));
            }
            else
            {
                mainMenuCanvas.SetActive(true);
            }
        }

        public void StartStage(int stageNumber)
        {
            loadingCanvas.SetActive(true);

            if (sceneMode > 0)
            {
                StartCoroutine(ChangeStage(sceneMode, stageNumber));
            }
            else
            {
                mainMenuCanvas.SetActive(false);
                StartCoroutine(LoadStage(stageNumber));
            }
        }

        private IEnumerator LoadStage(int stageNumber)
        {
            sceneMode = stageNumber;
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(stageNumber, LoadSceneMode.Additive);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(stageNumber));
            GameController.Instance.PauseGame(false);
            yield return new WaitForSeconds(loadingDelay);
            loadingCanvas.SetActive(false);
        }

        private IEnumerator UnloadStage(int stageNumber)
        {
            sceneMode = 0;
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
            AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(stageNumber);

            while (!asyncOperation.isDone)
            {
                yield return null;
            }

            GameController.Instance.PauseGame(false);
            mainMenuCanvas.SetActive(true);
            loadingCanvas.SetActive(false);
        }

        private IEnumerator ChangeStage(int oldStageNumber, int newStageNumber)
        {
            sceneMode = newStageNumber;
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(oldStageNumber);

            while (!asyncUnload.isDone)
            {
                yield return null;
            }

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(newStageNumber, LoadSceneMode.Additive);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(newStageNumber));
            GameController.Instance.PauseGame(false);
            yield return new WaitForSeconds(loadingDelay);
            loadingCanvas.SetActive(false);
        }
    }
}
