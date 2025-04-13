using UnityEngine;

namespace AE
{
	public class GameDirector : MonoBehaviour
	{
		[Header("General References")]
		[SerializeField]
		private Interactor playerInteractor;

		[Header("Initial task")]
		[SerializeField]
		private InteractiveObject candleInteractiveObject;
		[SerializeField]
		private GameObject initialFloor;
		[SerializeField]
		private GameObject evilFloor;
		[SerializeField]
		private Interaction candleStandInteraction;

		[Header("Main game")]
		[SerializeField]
		private float field1;

		[Header("Finish")]
		[SerializeField]
		private float field2;

		private void Start()
		{
			evilFloor.SetActive(false);
			initialFloor.SetActive(true);
			candleStandInteraction.enabled = false;
			candleInteractiveObject.OnInteracted += CandleInteractiveObject_OnInteracted;
		}

		private void CandleInteractiveObject_OnInteracted(InteractiveObject interactiveObject, Interactor interactor, Interaction interaction)
		{
			if (interactor == playerInteractor && interaction is CollectLightSourceInteraction)
				BeginMainGame();
		}

		private void BeginMainGame()
		{
			evilFloor.SetActive(true);
			initialFloor.SetActive(false);
			candleStandInteraction.enabled = true;
		}

		private void OnDestroy()
		{
			candleInteractiveObject.OnInteracted -= CandleInteractiveObject_OnInteracted;
		}
	}
}
