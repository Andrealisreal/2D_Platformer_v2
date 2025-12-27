using System;
using System.Collections;
using UnityEngine;

namespace Players.Abilities.Vampirisms
{
    [RequireComponent(typeof(VampirismDetector))]
    public class Vampirism : MonoBehaviour
    {
        [SerializeField] private float _healthStealCount = 2f;
        [SerializeField] private float _cooldown = 4f;
        [SerializeField] private float _duration = 6f;
        [SerializeField] private float _delaySteal = 0.2f;

        public event Action<float> Activated;
        public event Action<float> Deactivated;

        private VampirismDetector _detector;
        private Health _health;
        private float _radius;

        private bool _isOnActivated;
        
        public float Radius => _radius;

        private void Awake()
        {
            _detector = GetComponent<VampirismDetector>();
            _radius = _detector.Radius;
        }

        public void Activate(Health health)
        {
            if (_isOnActivated)
                return;

            _health = health;
            StartCoroutine(StealRoutine());
            Activated?.Invoke(_duration);
        }

        private IEnumerator StealRoutine()
        {
            _isOnActivated = true;

            var cooldownSpell = new WaitForSeconds(_cooldown);
            var delaySteal = new WaitForSeconds(_delaySteal);

            var elapsed = 0f;

            while (elapsed < _duration)
            {
                var enemy = _detector.GetEnemy();

                if (enemy == null)
                {
                    elapsed += _delaySteal;
                    
                    yield return delaySteal;
                    
                    continue;
                }

                enemy.Health.TakeDamage(_healthStealCount);
                _health.Heal(_healthStealCount);

                elapsed += _delaySteal;

                yield return delaySteal;
            }

            Deactivated?.Invoke(_cooldown);
            
            yield return cooldownSpell;
            
            _isOnActivated = false;
        }
    }
}