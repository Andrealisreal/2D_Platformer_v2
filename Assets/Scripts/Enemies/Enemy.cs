using System;
using Enemies.Movement;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Patroller))]
    [RequireComponent(typeof(Death))]
    [RequireComponent(typeof(Chaser))]
    public class Enemy : MonoBehaviour
    {
        [Header("Настройка здоровья")] [SerializeField]
        private float _currentHealth = 50f;

        [Header("Gizmo и Overlap")] [SerializeField]
        private LayerMask _playerLayerMask;

        [SerializeField] private float _radius = 3f;
        [SerializeField] private Color _gizmoColor = Color.green;

        public Health Health => _health;

        private Attacker _attacker;
        private Chaser _chaser;
        private Death _death;
        private Patroller _patroller;
        private Health _health;

        private State _currentState = State.Patrol;

        private enum State
        {
            Patrol,
            Chase,
            Attack
        }

        private void Awake()
        {
            _attacker = GetComponentInChildren<Attacker>(true);
            _chaser = GetComponent<Chaser>();
            _death = GetComponent<Death>();
            _patroller = GetComponent<Patroller>();
            _health = new Health(_currentHealth);
        }

        private void FixedUpdate()
        {
            UpdateState();
            
            switch (_currentState)
            {
                case State.Patrol:
                    _patroller.Patrol();
                    break;
                
                case State.Chase:
                    _chaser.Chase();
                    break;
                
                case State.Attack:
                    _attacker.Attack();
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnEnable()
        {
            _health.OnDeath += _death.Die;
        }

        private void OnDisable()
        {
            _health.OnDeath -= _death.Die;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        private bool TryPlayer()
        {
            return Physics2D.OverlapCircle(transform.position, _radius, _playerLayerMask) != false;
        }

        private void UpdateState()
        {
            if (TryPlayer())
                _currentState = State.Attack;
            else if (_chaser.TryPlayer(out _))
                _currentState = State.Chase;
            else
                _currentState = State.Patrol;
        }
    }
}