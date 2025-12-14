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
    [RequireComponent(typeof(EnemyDetector))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _currentHealth = 50f;

        public Health Health => _health;

        private EnemyDetector _detector;
        private Attacker _attacker;
        private Chaser _chaser;
        private Death _death;
        private Patroller _patroller;
        private Health _health;
        
        private void Awake()
        {
            _attacker = GetComponentInChildren<Attacker>(true);
            _detector = GetComponent<EnemyDetector>();
            _chaser = GetComponent<Chaser>();
            _death = GetComponent<Death>();
            _patroller = GetComponent<Patroller>();
            _health = new Health(_currentHealth);
        }

        private void FixedUpdate()
        {
            if (_detector.TryDetect(out var player) == false)
                _patroller.Patrol();
            else if (_attacker.IsInRange(player))
                _attacker.Attack();
            else
                _chaser.Chase(player.transform);
        }

        private void OnEnable()
        {
            _health.Died += _death.Die;
        }

        private void OnDisable()
        {
            _health.Died -= _death.Die;
        }
    }
}