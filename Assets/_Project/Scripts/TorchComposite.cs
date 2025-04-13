using UnityEngine;

namespace AE
{
	public class TorchComposite : Torch
    {
        [SerializeField]
        private Torch[] subtorches;

		private void OnEnable()
		{
			foreach (var torch in subtorches)
				torch.enabled = true;
		}

        private void OnDisable()
		{
			foreach (var torch in subtorches)
				torch.enabled = false;
		}
	}
}
