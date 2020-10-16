using UnityEngine;
using UnityEngine.Events;

namespace Combat
{
    [DisallowMultipleComponent]
    public class CombatUnit : MonoBehaviour
    {
        [System.Serializable]
        public class HealthChangeEvent : UnityEvent<float, float> {}

        public float maxHealth = 100.0f;

        public float currentHealth = 100.0f;

        [Header("Events")]
        public HealthChangeEvent onMaxHealthChange;
        public HealthChangeEvent onHealthChange;
        public UnityEvent onDeath;

        private void Awake()
        {
            if (onMaxHealthChange == null)
            {
                onMaxHealthChange = new HealthChangeEvent();
            }

            if (onHealthChange == null)
            {
                onHealthChange = new HealthChangeEvent();
            }

            if (onDeath == null)
            {
                onDeath = new UnityEvent();
            }
        }

        private void Start()
        {
            onMaxHealthChange.Invoke(maxHealth, maxHealth);
            onHealthChange.Invoke(currentHealth, currentHealth);
        }

        public void TakeDamage(float damage)
        {
            float previousHealth = currentHealth;
            currentHealth -= damage;

            if (currentHealth <= 0.0f)
            {
                currentHealth = 0.0f;
            }

            if (Mathf.Abs(currentHealth - previousHealth) > float.Epsilon)
            {
                onHealthChange.Invoke(currentHealth, previousHealth);
            }

            if (currentHealth == 0.0f)
            {
                onDeath.Invoke();
            }
        }
    }
}
