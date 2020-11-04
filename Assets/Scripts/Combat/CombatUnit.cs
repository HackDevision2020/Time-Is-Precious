using UnityEngine;
using UnityEngine.Events;

namespace Combat
{
    [DisallowMultipleComponent]
    public class CombatUnit : MonoBehaviour
    {
        [System.Serializable]
        public class HealthChangeEvent : UnityEvent<int, int> {}

        public int maxHealth = 3;

        public int currentHealth = 3;

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

        public void TakeDamage(int damage)
        {
            int previousHealth = currentHealth;
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
            }

            if (Mathf.Abs(currentHealth - previousHealth) > 0)
            {
                onHealthChange.Invoke(currentHealth, previousHealth);
            }

            if (currentHealth == 0)
            {
                onDeath.Invoke();
            }
        }
    }
}
