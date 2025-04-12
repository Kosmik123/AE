using UnityEngine;
using UnityEngine.InputSystem;

namespace AE
{
    public class Interactor : MonoBehaviour
    {
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
            currentInteractiveObject = null;
            if (Physics.Raycast(transform.position, transform.forward, out var hitInfo, interactionRange, detectedLayers))
            {
                var interactiveObject = hitInfo.collider.GetComponentInParent<InteractiveObject>();
                if (interactiveObject && interactiveObject.isActiveAndEnabled)
                {
                    currentInteractiveObject = interactiveObject;
                }
            }
        }

        private void OnDisable()
        {
            interactAction.performed -= Interact;
            interactAction.Disable();
            interactAction.Dispose();
            interactAction = null;
        }
    }
}
