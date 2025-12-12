using Enemies.Movement;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Patroller))]
    public class Enemy : MonoBehaviour
    {
        [Header("Настройка здоровья")]
        [SerializeField] private float _currentHealth = 50f;
        
        public Health Health => _health;
        
        private Patroller _patroller;
        private Health _health;
        private Animator _animator;
        
        private void  Awake()
        {
            _animator = GetComponent<Animator>();
            _patroller = GetComponent<Patroller>();
            _health = new Health(_currentHealth, _animator);
        }

        private void FixedUpdate()
        {
            _patroller.Patrol();
        }

        public void TakeDamage(float amount)
        {
            _health.TakeDamage(amount);
        }
    }
}
