using System.Collections.Generic;
using UnityEngine;

namespace AE
{
    [RequireComponent(typeof(Interactor))]
    public class GrabInteractionHandler : InteractorInteractionHandler
    {
        public event System.Action OnObjectDropped;

        [SerializeField]
        private Transform objectHolder;
        [SerializeField]
        private Interactor interactor;
        [SerializeField]
        private int grabbedObjectLayer; 

        [Header("States")]
        [SerializeField]
        private Rigidbody grabbedObject;

        private readonly Dictionary<MeshRenderer, int> originalRenderersLayers = new Dictionary<MeshRenderer, int>();

		public bool TryGetGrabbedObject(out Rigidbody newGrabbedObject)
		{
			newGrabbedObject = grabbedObject;
			return grabbedObject != null;
		}

		public void GrabObject(Rigidbody newGrabbedObject)
        {
            grabbedObject = newGrabbedObject;
            grabbedObject.isKinematic = true;

            foreach (var meshRenderer in grabbedObject.GetComponentsInChildren<MeshRenderer>())
			{
				originalRenderersLayers[meshRenderer] = meshRenderer.gameObject.layer;
				meshRenderer.gameObject.layer = grabbedObjectLayer; 
			}

			var grabbedObjectTransform = grabbedObject.transform;
			grabbedObjectTransform.SetParent(objectHolder, false);
			grabbedObjectTransform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            interactor.enabled = false;
            interactor.InteractInput.action.Enable();
			interactor.InteractInput.action.performed += Action_performed;
        }

		private void Action_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) => DropObject();

        private void DropObject()
        {
            interactor.InteractInput.action.Disable();
			interactor.InteractInput.action.performed -= Action_performed;
            
            foreach (var (meshRenderer, originalLayer) in originalRenderersLayers)
                meshRenderer.gameObject.layer = originalLayer;
            originalRenderersLayers.Clear();

            interactor.enabled = true;
            grabbedObject.transform.parent = null;
            grabbedObject.isKinematic = false;
            grabbedObject = null;
            OnObjectDropped?.Invoke();
        }
    }
}
