using UnityEngine;

namespace AE
{
    public class CratesPileController : MonoBehaviour
    {
        [SerializeField]
        private string barrelTag;
		[SerializeField]
		private Rigidbody[] crates;
		[SerializeField]
		private Rigidbody topSkull;

		[SerializeField]
		private float pushForce;

		private void OnTriggerEnter(Collider otherCollider)
		{
			Component other = otherCollider;
			if (otherCollider.attachedRigidbody)
				other = otherCollider.attachedRigidbody;

			if (other.CompareTag(barrelTag) == false)
				return;

			topSkull.isKinematic = false;
			foreach (var crate in crates)
			{
				crate.isKinematic = false;
				var forceDirection = (crate.position - other.transform.position);
				forceDirection.y = 0;
				forceDirection.Normalize();
				crate.AddForce(forceDirection * pushForce, ForceMode.Impulse);
			}
		}
	}
}
