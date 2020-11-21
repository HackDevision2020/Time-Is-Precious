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

        public bool isInvincible = false;

        [Header("Events")]
        public HealthChangeEvent onMaxHealthChange;
        public HealthChangeEvent onHealthChange;
        public UnityEvent onDeath;
        public UnityEvent onAttackStart;
        public UnityEvent onAttackEnd;

        public bool IsDead => isDead;

        private bool isDead;

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

            if (onAttackStart == null)
            {
                onAttackStart = new UnityEvent();
            }

            if (onAttackEnd == null)
            {
                onAttackEnd = new UnityEvent();
            }
        }

        private void Start()
        {
            onMaxHealthChange.Invoke(maxHealth, maxHealth);
            onHealthChange.Invoke(currentHealth, currentHealth);
        }

        public void SetInvincible(bool shouldBeInvincible)
        {
            isInvincible = shouldBeInvincible;
        }

        public void TakeDamage(int damage)
        {
            if (isInvincible)
            {
                return;
            }

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
                isDead = true;
                onDeath.Invoke();
            }
            else if (currentHealth < previousHealth)
            {
                isInvincible = true;
            }
        }

        public void StartAttack()
        {
            onAttackStart.Invoke();
        }

        public void EndAttack()
        {
            onAttackEnd.Invoke();
        }
    }
}
