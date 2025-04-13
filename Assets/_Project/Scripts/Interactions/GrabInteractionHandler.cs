using UnityEngine;

namespace AE
{
    [RequireComponent(typeof(Interactor))]
    public class GrabInteractionHandler : InteractorInteractionHandler
    {
        [SerializeField]
        private Transform objectHolder;

        [Header("Candle")]
        [SerializeField]
        private string lightSourceTag;

        [Header("States")]
        [SerializeField]
        private Rigidbody grabbedObject;

        public void GrabObject(Rigidbody newGrabbedObject)
        {
            grabbedObject = newGrabbedObject;

            grabbedObject.isKinematic = true;

			var grabbedObjectTransform = grabbedObject.transform;
			grabbedObjectTransform.SetParent(objectHolder, false);
			grabbedObjectTransform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        public bool TryGetGrabbedObject(out Rigidbody newGrabbedObject)
        {
			newGrabbedObject = grabbedObject;
            return grabbedObject != null;
        }

        public void DropObject()
        {
            grabbedObject.transform.parent = null;
            grabbedObject.isKinematic = false;
        }
    }
}
