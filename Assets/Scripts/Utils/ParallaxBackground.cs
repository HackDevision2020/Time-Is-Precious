// Code base from https://youtu.be/wBol2xzxCOU
// Modified for clarify and performance

using UnityEngine;

namespace Utils
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    public class ParallaxBackground : MonoBehaviour
    {
        [Range(0.0f, 1.0f)]
        public float xFactor = 1.0f;

        [Range(0.0f, 1.0f)]
        public float yFactor = 1.0f;

        public bool tileX = true;

        public bool tileY;

        private Transform cameraTransform;
        private Vector3 previousCameraPosition;

        private SpriteRenderer spriteRenderer;
        private float textureUnitSizeX;
        private float textureUnitSizeY;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            Sprite sprite = spriteRenderer.sprite;
            Texture2D texture = sprite.texture;
            textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
            textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
        }

        private void Start()
        {
            cameraTransform = Camera.main.transform; // I hope this is ok... (`Camera.main` can be null)
            previousCameraPosition = cameraTransform.position;
        }

        private void LateUpdate()
        {
            Vector3 currentCameraPosition = cameraTransform.position;
            Vector3 movement = currentCameraPosition - previousCameraPosition;
            previousCameraPosition = currentCameraPosition;

            Vector3 currentPosition = transform.position;
            currentPosition += new Vector3(movement.x * xFactor, movement.y * yFactor, 0.0f);
            Vector3 positionDiff = currentCameraPosition - currentPosition;

            if (tileX && Mathf.Abs(positionDiff.x) >= textureUnitSizeX)
            {
                float offsetPosition = positionDiff.x % textureUnitSizeX;
                currentPosition.x = currentCameraPosition.x + offsetPosition;
            }

            if (tileY && Mathf.Abs(positionDiff.y) >= textureUnitSizeY)
            {
                float offsetPosition = positionDiff.y % textureUnitSizeY;
                currentPosition.y = currentCameraPosition.y + offsetPosition;
            }

            transform.position = currentPosition;
        }
    }
}
