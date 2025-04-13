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
        [SerializeField]
        private Rigidbody _rigidbody;

        private void Reset()
        {
            rotator = GetComponentInChildren<Rotator>();
            splineMovement = GetComponent<SplineAnimate>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Awake()
        {
            rotator.enabled = false;
            splineMovement.enabled = false;
            _rigidbody.isKinematic = true;
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

			splineMovement.Completed += SplineMovement_Completed;
        }

		private void SplineMovement_Completed()
		{
			splineMovement.Completed -= SplineMovement_Completed;
            rotator.enabled = false;
            _rigidbody.isKinematic = false;
        }
	}
}
