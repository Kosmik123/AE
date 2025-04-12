using UnityEngine;

namespace AE
{
    public class GrabInteraction : Interaction
    {
        [SerializeField]
        private GrabHand targetHand;
        [SerializeField]
        private Rigidbody grabbedObject;

        private void Start()
        {
            Debug.Log(grabbedObject);
        }

        public override bool CanInteract(Interactor interactor) => base.CanInteract(interactor) 
            && interactor.TryGetComponent<Grabber>(out var grabber);


        public override bool TryInteract(Interactor interactor)
        {
            if (interactor.TryGetComponent<Grabber>(out var grabber) == false)
                return false;

            if (grabber.TryGetGrabbedObject(targetHand, out _))
                return false;

            grabber.GrabObject(grabbedObject, targetHand);
            enabled = false;
            return true;
        }
    }
    
    public enum GrabHand
    {
        Right,
        Left,
    }
}
