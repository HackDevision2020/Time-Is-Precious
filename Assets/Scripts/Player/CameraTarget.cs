using Controllers;
using UnityEngine;

namespace Player
{
    public class CameraTarget : MonoBehaviour
    {
        private CameraMotionController mainCameraController;

        private void OnEnable()
        {
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                CameraMotionController controller = mainCamera.GetComponent<CameraMotionController>();
                if (controller != null)
                {
                    mainCameraController = controller;
                    mainCameraController.followTarget = transform;
                }
            }
        }

        private void OnDisable()
        {
            if (mainCameraController != null)
            {
                mainCameraController.followTarget = null;
                mainCameraController = null;
            }
        }
    }
}
