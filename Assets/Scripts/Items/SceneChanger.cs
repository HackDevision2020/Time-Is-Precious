using Controllers;
using UnityEngine;

namespace Items
{
    [DisallowMultipleComponent]
    public class SceneChanger : MonoBehaviour
    {
        [Tooltip("Set to 0 to go to the main menu")]
        [Range(0, 3)]
        public int stage = 0;

        public void MoveStage()
        {
            if (stage == 0)
            {
                GameController.Instance.sceneLoadManager.GoToMenu();
            }
            else
            {
                GameController.Instance.sceneLoadManager.StartStage(stage);
            }
        }
    }
}
