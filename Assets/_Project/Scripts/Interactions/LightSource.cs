using UnityEngine;

namespace AE
{
    public class LightSource : MonoBehaviour
    {
        [field: SerializeField]
        public bool IsLighted { get; set; }

        [field: SerializeField]
        public Transform Candle { get; set; }
    }
}
