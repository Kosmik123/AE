using UnityEngine;

namespace AE
{
    [RequireComponent(typeof(Interactor))]
    public class GrabInteractionHandler : InteractorInteractionHandler
    {
        [SerializeField]
        private Transform objectHolder;
        [SerializeField]
        private Interactor interactor;

        [Header("States")]
        [SerializeField]
        private Rigidbody grabbedObject;

		public bool TryGetGrabbedObject(out Rigidbody newGrabbedObject)
		{
			newGrabbedObject = grabbedObject;
			return grabbedObject != null;
		}

		public void GrabObject(Rigidbody newGrabbedObject)
        {
            grabbedObject = newGrabbedObject;
            grabbedObject.isKinematic = true;

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
            
            interactor.enabled = true;
            grabbedObject.transform.parent = null;
            grabbedObject.isKinematic = false;
            grabbedObject = null;
        }
    }
}
