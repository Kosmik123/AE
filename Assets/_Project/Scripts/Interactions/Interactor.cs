using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AE
{
	public abstract class InteractorInteractionHandler : MonoBehaviour
	{ }

	public class Interactor : MonoBehaviour
	{
		public delegate void InteractiveObjectChangeEventHandler(Interactor interactor, InteractiveObject from, InteractiveObject to);

		public event InteractiveObjectChangeEventHandler OnInteractiveObjectChanged;

		[SerializeField]
		private Transform rayOrigin;
		[SerializeField]
		private LayerMask detectedLayers;
		[SerializeField]
		private float interactionRange = 2f;
		[SerializeField]
		private InputActionReference interactInput;
		public InputActionReference InteractInput => interactInput;
		[SerializeField]
		private List<InteractorInteractionHandler> interactionHandlers;
		[SerializeField]
		private Transform mainBody;

		[SerializeField]
		private Behaviour[] movementComponents;

		[Header("States")]
		[SerializeField]
		private InteractiveObject currentInteractiveObject;

		public Transform MainBody => mainBody;

		private void OnEnable()
		{
			interactInput.action.Enable();
			interactInput.action.performed += Interact;
		}

		private void Interact(InputAction.CallbackContext context)
		{
			if (currentInteractiveObject == null)
				return;

			currentInteractiveObject.TryInteract(this);
		}

		private void Update()
		{
			var previousInteractiveObject = currentInteractiveObject;
			currentInteractiveObject = null;
			if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out var hitInfo, interactionRange, detectedLayers))
			{
				var interactiveObject = hitInfo.collider.GetComponentInParent<IInteractiveObject>();
				if (interactiveObject != null && interactiveObject.CanInteract(this))
				{
					currentInteractiveObject = interactiveObject as InteractiveObject;
				}
			}

			if (previousInteractiveObject != currentInteractiveObject)
				OnInteractiveObjectChanged?.Invoke(this, previousInteractiveObject, currentInteractiveObject);
		}

		public void DisableMovement()
		{
			foreach (var component in movementComponents)
			{
				component.enabled = false;
			}
		}

		public void EnableMovement()
		{
			foreach (var component in movementComponents)
			{
				component.enabled = true;
			}
		}

		public bool TryGetInteractionHandler<T>(out T handler)
			where T : InteractorInteractionHandler
		{
			handler = null;
			for (int i = 0; i < interactionHandlers.Count; i++)
			{
				if (interactionHandlers[i] is T typedHandler)
				{
					handler = typedHandler;
					return true;
				}
			}
			return false;
		}

		private void OnDisable()
		{
			interactInput.action.performed -= Interact;
		}

		private void OnDrawGizmosSelected()
		{
			if (rayOrigin)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawRay(rayOrigin.position, rayOrigin.forward * interactionRange);
			}
		}
	}
}
