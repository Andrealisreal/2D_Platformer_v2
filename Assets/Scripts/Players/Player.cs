using Players.Animation;
using Players.Input;
using Players.Inventory;
using Players.Movement;
using UI;
using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Jumper))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Death))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private LayerMask _deathZone;
        [SerializeField] private Animator _animator;
        [SerializeField] private HealthView _healthView;
        
        [Header("Настройка здоровья")]
        [SerializeField] private float _currentHealth = 100f;
        
        private Mover _mover;
        private Jumper _jumper;
        private Wallet _wallet;
        private PlayerInput _input;
        private Death _death;
        private PlayerAnimator _playerAnimator;
        private Health _health;
        private Trigger _trigger;
        private Attacker _attacker;
        
        public Health Health => _health;

        private void Awake()
        {
            _attacker = GetComponentInChildren<Attacker>(true);
            _mover = GetComponent<Mover>();
            _jumper = GetComponent<Jumper>();
            _input = GetComponent<PlayerInput>();
            _death = GetComponent<Death>();
            _wallet = new Wallet();
            _playerAnimator = new PlayerAnimator(_animator);
            _health = new Health(_currentHealth);
            _trigger = new Trigger(_wallet, _health, _playerAnimator, _death);
            _healthView.Initialize(_health);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _trigger.OnEnter(other);
        }

        private void OnEnable()
        {
            _input.JumpClicked += _jumper.Jump;
            _input.JumpClicked += JumpAnimation;
            _input.AttackClicked += _attacker.Attack;
            _health.Died += _death.Die;
        }

        private void OnDisable()
        {
            _input.JumpClicked -= _jumper.Jump;
            _input.JumpClicked -= JumpAnimation;
            _input.AttackClicked -= _attacker.Attack;
            _health.Died -= _death.Die;
        }

        private void FixedUpdate()
        {
            _mover.Move(_input.Movement);
            _playerAnimator.PlayRun(_input.Movement.x);
        }
        
        private void JumpAnimation()
        {
            if(_jumper.TryJump() == false)
                return;
            
            _playerAnimator.PlayJump();
        }
    }
}
