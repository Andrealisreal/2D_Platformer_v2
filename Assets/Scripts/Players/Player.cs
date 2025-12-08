using Coins;
using Players.Animation;
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
        [SerializeField] private LayerMask _deathZone;
        
        private Mover _mover;
        private Jumper _jumper;
        private Wallet _wallet;
        private PlayerInput _input;
        private Death _death;
        private PlayerAnimator _animator;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _jumper = GetComponent<Jumper>();
            _wallet = GetComponent<Wallet>();
            _input = GetComponent<PlayerInput>();
            _death = new Death();
            _animator = new PlayerAnimator(GetComponent<Animator>());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<Coin>(out var coin))
            {
                coin.Collect();
                _wallet.AddCoin();
                
                return;
            }

            if (other.TryGetComponent<DeathZone>(out _))
            {
                Debug.Log("Попал в зону смерти");
                _animator.PlayDeath();
                //_death.Die(this.gameObject);
            }
        }

        private void OnEnable()
        {
            _input.JumpClicked += _jumper.Jump;
            _input.JumpClicked += _animator.PlayJump;
        }

        private void OnDisable()
        {
            _input.JumpClicked -= _jumper.Jump;
            _input.JumpClicked -= _animator.PlayJump;
        }

        private void FixedUpdate()
        {
            _mover.Move(_input.Movement);
            _animator.PlayRun(_input.Movement.x);
        }
    }
}
