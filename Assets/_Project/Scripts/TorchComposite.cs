using System.Linq;
using UnityEngine;

namespace AE
{
	public class TorchComposite : Torch
    {
        [SerializeField]
        private Torch[] subtorches;

		private void Reset()
		{
			subtorches = GetComponentsInChildren<Torch>(true)
				.Where(t => t != this)
				.ToArray();
		}

		private void OnEnable()
		{
			foreach (var torch in subtorches)
				torch.enabled = true;

			InvokeEvent();
		}

        private void OnDisable()
		{
			foreach (var torch in subtorches)
				torch.enabled = false;
		}
	}
}
