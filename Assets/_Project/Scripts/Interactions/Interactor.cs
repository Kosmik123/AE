using UnityEngine;
using UnityEngine.InputSystem;

namespace AE
{
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
        private InputAction interactAction;

        [Header("States")]
        [SerializeField]
        private InteractiveObject currentInteractiveObject;

        private void OnEnable()
        {
            interactAction = interactInput.action.Clone();
            interactAction.Enable();
            interactAction.performed += Interact;
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

        private void OnDisable()
        {
            interactAction.performed -= Interact;
            interactAction.Disable();
            interactAction.Dispose();
            interactAction = null;
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
