using UnityEngine;
using UnityEngine.Events;

namespace Combat
{
    public class CombatAnimationEventHandler : MonoBehaviour
    {
        public UnityEvent onHurtAnimationEnd;
        public UnityEvent onDeathAnimationEnd;
        public UnityEvent onMeleeAttackColliderActivate;
        public UnityEvent onMeleeAttackColliderDeactivate;

        private void Awake()
        {
            if (onHurtAnimationEnd == null)
            {
                onHurtAnimationEnd = new UnityEvent();
            }

            if (onDeathAnimationEnd == null)
            {
                onDeathAnimationEnd = new UnityEvent();
            }

            if (onMeleeAttackColliderActivate == null)
            {
                onMeleeAttackColliderActivate = new UnityEvent();
            }

            if (onMeleeAttackColliderDeactivate == null)
            {
                onMeleeAttackColliderDeactivate = new UnityEvent();
            }
        }

        public void DeathAnimationEndHandler()
        {
            onDeathAnimationEnd.Invoke();
        }

        public void HurtAnimationEndHandler()
        {
            onHurtAnimationEnd.Invoke();
        }

        public void MeleeAttackColliderActivateHandler()
        {
            onMeleeAttackColliderActivate.Invoke();
        }

        public void MeleeAttackColliderDeactivateHandler()
        {
            onMeleeAttackColliderDeactivate.Invoke();
        }
    }
}
