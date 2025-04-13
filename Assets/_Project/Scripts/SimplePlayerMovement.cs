using UnityEngine;
using UnityEngine.InputSystem;

namespace AE
{
    [RequireComponent(typeof(CharacterController))]
    public class SimplePlayerMovement : MonoBehaviour
    {
        private CharacterController characterController;

        [SerializeField]
        private float moveSpeed = 4;

        [SerializeField]
        private InputActionReference moveInputAction;
        private InputAction moveInput;

        private void Awake()
        {
            moveInput = moveInputAction.action;
            characterController = GetComponent<CharacterController>();
        }

        private void OnEnable()
        {
            moveInput.Enable();
            characterController.enabled = true;
        }

        private void Update()
        {
            MovePlayer();
        }

        private void MovePlayer()
        {
            Vector2 input = moveInput.ReadValue<Vector2>();
            Vector3 direction = transform.forward * input.y + transform.right * input.x;
            direction.y = 0;

            characterController.SimpleMove(moveSpeed * direction);
        }
     
        private void OnDisable()
        {
            characterController.enabled = false;
            moveInput.Disable();
        }
    }
}
