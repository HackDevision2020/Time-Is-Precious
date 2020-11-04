using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthHeart : MonoBehaviour
    {
        public Sprite filledImage;

        public Sprite emptyImage;

        public bool Filled
        {
            get => image && image.sprite == filledImage;

            set
            {
                if (image)
                {
                    image.sprite = value ? filledImage : emptyImage;
                }
            }
        }

        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
        }
    }
}
