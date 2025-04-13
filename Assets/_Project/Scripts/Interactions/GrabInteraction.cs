using UnityEngine;

namespace AE
{
	public class GrabInteraction : Interaction
    {
        [SerializeField]
        private Rigidbody grabbedObject;

        private void Start()
        {
            Debug.Log(grabbedObject);
        }

        public override bool CanInteract(Interactor interactor) => base.CanInteract(interactor) 
            && interactor.TryGetInteractionHandler<GrabInteractionHandler>(out _);

        public override bool TryInteract(Interactor interactor)
        {
            if (interactor.TryGetInteractionHandler<GrabInteractionHandler>(out var grabber) == false)
                return false;

            if (grabber.TryGetGrabbedObject(out _))
                return false;

            grabber.GrabObject(grabbedObject);
            enabled = false;
            return true;
        }
    }
}
