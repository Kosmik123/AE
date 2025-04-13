using UnityEngine;

namespace AE
{
	public class CollectLightSourceInteraction : Interaction
	{
		[SerializeField]
		private GameObject itemSceneObject;

		public override bool CanInteract(Interactor interactor) => interactor.TryGetInteractionHandler<LightSource>(out var lightSource) 
			&& !lightSource.HasCandle;

		public override bool TryInteract(Interactor interactor)
		{
			if (interactor.TryGetInteractionHandler<LightSource>(out var lightSource) == false)
				return false;

			itemSceneObject.SetActive(false);
			lightSource.CollectCandle();
			return true;
		}
	}
}
