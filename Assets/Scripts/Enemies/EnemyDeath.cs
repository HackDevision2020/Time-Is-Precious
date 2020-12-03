using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Enemies
{
    [DisallowMultipleComponent]
    public class EnemyDeath : MonoBehaviour
    {
        public float flickerDuration = 0.5f;
        public int flickerNumber = 2;
        public float minOpacity = 0.2f;

        /// <summary>
        /// Any callbacks to be called when the reaction to the death is finished.
        /// Typically, you'll call some kind of destroy function for this game object.
        /// </summary>
        public UnityEvent onDeathReactionEnd;

        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            if (onDeathReactionEnd == null)
            {
                onDeathReactionEnd = new UnityEvent();
            }
        }

        /// <summary>
        /// The method called as a callback when the enemy dies. Use this function to implement any wanted effects.
        /// </summary>
        public void OnDeath()
        {
            if (spriteRenderer)
            {
                StartCoroutine(FlickerSprite());
            }
            else
            {
                onDeathReactionEnd.Invoke();
            }
        }

        /// <summary>
        /// Coroutine to make the sprite flicker. Use this to implement any things that must happen asynchronously.
        /// </summary>
        /// <returns></returns>
        private IEnumerator FlickerSprite()
        {
            bool isShown = true;
            float interval = flickerDuration / flickerNumber / 2.0f;
            Color fadeColor = new Color(1.0f, 1.0f, 1.0f, minOpacity);
            int loopTimes = 2 * flickerNumber;

            for (int i = 0; i < loopTimes; i++)
            {
                spriteRenderer.color = isShown ? fadeColor : Color.white;
                isShown = !isShown;
                yield return new WaitForSeconds(interval);
            }

            onDeathReactionEnd.Invoke();
        }
    }
}
