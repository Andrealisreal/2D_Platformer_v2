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
    [RequireComponent(typeof(Death))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private LayerMask _deathZone;
        
        [Header("Настройка здоровья")]
        [SerializeField] private float _currentHealth = 100f;
        
        private Mover _mover;
        private Jumper _jumper;
        private Wallet _wallet;
        private PlayerInput _input;
        private Death _death;
        private PlayerAnimator _animator;
        private Health _health;
        private Trigger _trigger;
        private Attacker _attacker;

        private void Awake()
        {
            _attacker = GetComponentInChildren<Attacker>(true);
            _mover = GetComponent<Mover>();
            _jumper = GetComponent<Jumper>();
            _wallet = GetComponent<Wallet>();
            _input = GetComponent<PlayerInput>();
            _death = GetComponent<Death>();
            _animator = new PlayerAnimator(GetComponent<Animator>());
            _health = new Health(_currentHealth, GetComponent<Animator>());
            _trigger = new Trigger(_wallet, _health, _animator, _death);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _trigger.OnEnter(other);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            _trigger.OnTouch(other);
        }

        private void OnEnable()
        {
            _input.JumpClicked += _jumper.Jump;
            _input.JumpClicked += JumpAnimation;
            _input.AttackClicked += _attacker.Attack;
        }

        private void OnDisable()
        {
            _input.JumpClicked -= _jumper.Jump;
            _input.JumpClicked -= JumpAnimation;
        }

        private void FixedUpdate()
        {
            _mover.Move(_input.Movement);
            _animator.PlayRun(_input.Movement.x);
        }
        
        private void JumpAnimation()
        {
            if(_jumper.TryJump() == false)
                return;
            
            _animator.PlayJump();
        }
    }
}
