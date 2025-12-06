using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Players.Input
{
    public class PlayerInput : MonoBehaviour
    {
        public event Action JumpClicked;

        public Vector2 Movement => _inputAction.Player.Move.ReadValue<Vector2>();

        private InputActions _inputAction;

        private void Awake()
        {
            _inputAction = new InputActions();

            _inputAction.Player.Jump.performed += Jump;
        }

        private void OnEnable()
        {
            _inputAction.Enable();
        }

        private void OnDisable()
        {
            _inputAction.Disable();
        }

        private void Jump(InputAction.CallbackContext context)
        {
            JumpClicked?.Invoke();
        }
    }
}