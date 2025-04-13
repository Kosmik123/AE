using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace AE
{
	public class StandUpInteraction : Interaction
    {
        [SerializeField]
        private Transform targetLocation;

		[SerializeField]
		private float transitionDuration = 1f;

		[Space, SerializeField]
		private UnityEvent onStandUp;

		public override bool TryInteract(Interactor interactor)
		{
			interactor.enabled = false;
			interactor.DisableMovement();


			var targetLookDirection = targetLocation.position - interactor.MainBody.position;
			float targetLookAngle = Mathf.Atan2(targetLookDirection.x, targetLookDirection.z) * Mathf.Rad2Deg;
			DOTween.Sequence()
				.Join(transform.DOMove(targetLocation.position, transitionDuration))
				.Join(transform.DORotateQuaternion(targetLocation.rotation, transitionDuration))
				.Join(interactor.MainBody.DORotate(new Vector3(0, targetLookAngle, 0), transitionDuration))
				.AppendCallback(Finalize)
				.Play();

			return true;

			void Finalize()
			{
				interactor.EnableMovement();
				interactor.enabled = true;
				enabled = false;
				onStandUp?.Invoke();
			}
		}
	}
}
