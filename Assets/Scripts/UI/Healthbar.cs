using UnityEngine;

namespace UI
{
    public class Healthbar : MonoBehaviour
    {
        public GameObject heartPrefab;

        private int currentMaxHealth = 1;
        private int currentHealth = 0;

        public void OnHealthUpdated(int newHealth, int oldHealth)
        {
            if (newHealth == currentHealth)
            {
                return;
            }

            for (int i = 0; i < currentMaxHealth; i++)
            {
                HealthHeart heart = transform.GetChild(i).GetComponent<HealthHeart>();
                if (heart)
                {
                    heart.Filled = i < newHealth;
                }
#if UNITY_EDITOR
                else
                {
                    Debug.LogError("The health bar must have children with HealthHeart component");
                }
#endif
            }

            currentHealth = newHealth;
        }

        public void OnMaxHealthUpdated(int newMaxHealth, int oldMaxHealth)
        {
            if (newMaxHealth > currentMaxHealth)
            {
                int numToAdd = newMaxHealth - currentMaxHealth;
                for (int i = 0; i < numToAdd; i++)
                {
                    Instantiate(heartPrefab, transform);
                }

                currentMaxHealth = newMaxHealth;
                OnHealthUpdated(currentHealth + numToAdd, currentHealth);
            }
            else if (newMaxHealth < currentMaxHealth)
            {
                int numToRemove = currentMaxHealth - newMaxHealth;
                for (int i = 0; i < numToRemove; i++)
                {
                    GameObject lastHeart = transform.GetChild(currentMaxHealth - i - 1).gameObject;
                    Destroy(lastHeart);
                }

                currentMaxHealth = newMaxHealth;
                OnHealthUpdated(Mathf.Min(currentMaxHealth, currentHealth), currentHealth);
            }
        }
    }
}
