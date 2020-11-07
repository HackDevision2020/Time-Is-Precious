using Combat;
using Controllers;
using UnityEngine;

namespace Player
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CombatUnit))]
    public class PlayerDamageHandler : MonoBehaviour
    {
        public void OnPlayerHealthChanged(int newHealth, int oldHealth)
        {
            GameController.Instance.gameStateTracker.healthbar.OnHealthUpdated(newHealth, oldHealth);
        }

        public void OnPlayerMaxHealthChanged(int newMaxHealth, int oldMaxHealth)
        {
            GameController.Instance.gameStateTracker.healthbar.OnMaxHealthUpdated(newMaxHealth, oldMaxHealth);
        }

        public void OnPlayerDeath()
        {
            int currentStage = GameController.Instance.sceneLoadManager.CurrentStage;
            GameController.Instance.sceneLoadManager.StartStage(currentStage);
        }
    }
}
