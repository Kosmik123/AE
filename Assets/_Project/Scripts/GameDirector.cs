using UnityEngine;
using UnityEngine.Pool;

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
		[SerializeField]
		private GameObject[] stairsBlocks;

		[Header("Finish")]
		[SerializeField]
		private GrabInteractionHandler grabInteractionHandler;
		[SerializeField]
		private CapsuleCollider dropArea;
		[SerializeField]
		private LayerMask interactiveObjectsLayers;
		[SerializeField]
		private string[] objectsTagsNeededInDropArea;
		[SerializeField]
		private Torch[] requiredTorches;

		private readonly Collider[] detectedCollidersInDropArea = new Collider[10];

		private void Start()
		{
			foreach (var block in stairsBlocks)
				block.SetActive(true);
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
			candleInteractiveObject.OnInteracted -= CandleInteractiveObject_OnInteracted;
			evilFloor.SetActive(true);
			initialFloor.SetActive(false);
			candleStandInteraction.enabled = true;
			foreach (var block in stairsBlocks)
				block.SetActive(false);

			grabInteractionHandler.OnObjectDropped += CheckVictoryCondition;
			Torch.OnTorchLighedUp += CheckVictoryCondition;
		}

		private void CheckVictoryCondition()
		{
			foreach (var torch in requiredTorches)
				if (torch.enabled == false)
					return;

			float capsulePointsDistance = dropArea.height - 2 * dropArea.radius;

			Vector3 dropAreaWorldCenter = dropArea.transform.position + dropArea.center;
			Vector3 dropAreaPoint1 = dropAreaWorldCenter + dropArea.transform.up * (capsulePointsDistance / 2);
			Vector3 dropAreaPoint2 = dropAreaWorldCenter - dropArea.transform.up * (capsulePointsDistance / 2);

			int detectedObjectsCount = Physics.OverlapCapsuleNonAlloc(dropAreaPoint1, dropAreaPoint2, dropArea.radius, detectedCollidersInDropArea, interactiveObjectsLayers);

			if (detectedObjectsCount < objectsTagsNeededInDropArea.Length)
				return;

			var tempTagsList = ListPool<string>.Get();
			tempTagsList.Clear();
			tempTagsList.AddRange(objectsTagsNeededInDropArea);

			for (int i = 0; i < detectedObjectsCount; i++)
			{
				var detectedCollider = detectedCollidersInDropArea[i];
				if (detectedCollider.TryGetComponent<InteractiveObject>(out var interactiveObject))
				{
					tempTagsList.Remove(interactiveObject.gameObject.tag);
				}
			}

			int missingObjectsCount = tempTagsList.Count;
			ListPool<string>.Release(tempTagsList);

			if (missingObjectsCount > 0)
				return;

			grabInteractionHandler.OnObjectDropped -= CheckVictoryCondition;
			Torch.OnTorchLighedUp -= CheckVictoryCondition;

			// ZWYCIÊSTWO
			Debug.Log("WYGRA£EŒ!");

		}

		private void OnDestroy()
		{
			candleInteractiveObject.OnInteracted -= CandleInteractiveObject_OnInteracted;
			grabInteractionHandler.OnObjectDropped -= CheckVictoryCondition;
			Torch.OnTorchLighedUp -= CheckVictoryCondition;
		}
	}
}
