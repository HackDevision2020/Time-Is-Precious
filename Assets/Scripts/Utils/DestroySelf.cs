using UnityEngine;

namespace Utils
{
    [DisallowMultipleComponent]
    public class DestroySelf : MonoBehaviour
    {
        public void DoDestroy()
        {
            Destroy(gameObject);
        }
    }
}
