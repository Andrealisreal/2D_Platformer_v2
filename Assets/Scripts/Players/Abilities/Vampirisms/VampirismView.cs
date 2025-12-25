using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Players.Abilities.Vampirisms
{
    [RequireComponent(typeof(Slider))]
    public class VampirismView : MonoBehaviour
    {
        [SerializeField] private Vampirism _vampirism;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _animationDuration = 0.3f;
        
        private Slider _slider;
        private Coroutine _currentCoroutine;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            _vampirism.Activated += OnValueChanged;
        }

        private void OnDisable()
        {
            _vampirism.Activated -= OnValueChanged;
        }
        
        private void OnValueChanged(float duration, float cooldown)
        {
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
            
            _currentCoroutine = StartCoroutine(ChangeValueRoutine(duration, cooldown));
        }
        
        private IEnumerator ChangeValueRoutine(float current, float max)
        {
            var target = current / max;
        
            while (_slider.value != target)
            {
                _slider.value = Mathf.MoveTowards(_slider.value, target, _speed * Time.deltaTime);
            
                yield return null;
            }
        }
    }
}
