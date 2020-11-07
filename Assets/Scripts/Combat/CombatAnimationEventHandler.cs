using UnityEngine;
using UnityEngine.Events;

namespace Combat
{
    public class CombatAnimationEventHandler : MonoBehaviour
    {
        public UnityEvent onHurtAnimationEnd;
        public UnityEvent onDeathAnimationEnd;

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
        }

        public void DeathAnimationEndHandler()
        {
            onDeathAnimationEnd.Invoke();
        }

        public void HurtAnimationEndHandler()
        {
            onHurtAnimationEnd.Invoke();
        }
    }
}
