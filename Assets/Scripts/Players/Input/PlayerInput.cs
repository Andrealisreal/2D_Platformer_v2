using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Players.Input
{
    public class PlayerInput : MonoBehaviour
    {
        public event Action JumpClicked;
        public event Action AttackClicked;
        public event Action VampirismClicked;

        public Vector2 Movement => _inputAction.Player.Move.ReadValue<Vector2>();

        private InputActions _inputAction;

        private void Awake()
        {
            _inputAction = new InputActions();

            _inputAction.Player.Jump.performed += Jump;
            _inputAction.Player.Attack.performed += Attack;
            _inputAction.Player.Abilities.performed += StealHealth;
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
        
        private void Attack(InputAction.CallbackContext context)
        {
            AttackClicked?.Invoke();
        }
        
        private void StealHealth(InputAction.CallbackContext context)
        {
            VampirismClicked?.Invoke();
        }
    }
}