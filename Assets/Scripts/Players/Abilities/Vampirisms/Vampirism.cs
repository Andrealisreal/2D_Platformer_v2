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

        public event Action<float, float> Activated;

        private VampirismDetector _detector;
        private Health _health;

        private bool _isOnActivated;

        private void Awake()
        {
            _detector = GetComponent<VampirismDetector>();
        }

        public void Activate(Health health)
        {
            if (_isOnActivated)
                return;

            _health = health;
            StartCoroutine(StealRoutine());
            Activated?.Invoke(_duration, _cooldown);
        }

        private IEnumerator StealRoutine()
        {
            _isOnActivated = true;

            Debug.Log("Способность активна " + Time.time);

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

            Debug.Log("Начался кулдаун " + Time.time);

            yield return cooldownSpell;
            
            Debug.Log("Конец куладуна " + Time.time);

            _isOnActivated = false;
        }
    }
}