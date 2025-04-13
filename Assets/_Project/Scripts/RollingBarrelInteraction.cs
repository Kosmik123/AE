using UnityEngine;

namespace AE
{
	public class RollingBarrelInteraction : Interaction
	{
		[SerializeField]
		private RollingBarrel rollingBarrel;

		public override bool TryInteract(Interactor interactor)
		{
			rollingBarrel.Roll();
			enabled = false;
			return true;
		}
	}
}
