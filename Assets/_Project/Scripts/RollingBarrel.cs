using UnityEngine;
using UnityEngine.Splines;

namespace AE
{
    public class RollingBarrel : MonoBehaviour
    {
        [SerializeField]
        private float rollSpeed;
        [SerializeField]
        private Rotator rotator;
        [SerializeField]
        private SplineAnimate splineMovement;
        [SerializeField]
        private float radius;

        private void Reset()
        {
            rotator = GetComponentInChildren<Rotator>();
            splineMovement = GetComponent<SplineAnimate>();
        }

        private void Awake()
        {
            rotator.enabled = false;
            splineMovement.enabled = false;
        }

        [ContextMenu("Roll")]
        public void Roll()
        {
            float angularSpeed = 360 * rollSpeed / (2 * Mathf.PI * radius);
            rotator.RotationSpeed = new Vector3(0, -angularSpeed, 0);

            splineMovement.AnimationMethod = SplineAnimate.Method.Speed;
            splineMovement.MaxSpeed = rollSpeed;
            splineMovement.enabled = true;
            rotator.enabled = true;
        }
    }
}
