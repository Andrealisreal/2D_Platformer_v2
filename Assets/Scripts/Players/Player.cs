using Coins;
using Players.Input;
using Players.Inventory;
using Players.Movement;
using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Jumper))]
    [RequireComponent(typeof(Wallet))]
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour
    {
        private Mover _mover;
        private Jumper _jumper;
        private Wallet _wallet;
        private PlayerInput _input;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _jumper = GetComponent<Jumper>();
            _wallet = GetComponent<Wallet>();
            _input = GetComponent<PlayerInput>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<Coin>(out var coin) == false)
                return;
            
            _wallet.AddCoin();
            coin.Collect();
        }

        private void OnEnable()
        {
            _input.JumpClicked += _jumper.Jump;
        }

        private void OnDisable()
        {
            _input.JumpClicked -= _jumper.Jump;
        }

        private void FixedUpdate()
        {
            _mover.Move(_input.Movement);
        }
    }
}
