using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Slider))]
    public class SmoothSliderBar : HealthView
    {
        [SerializeField] private float _speed = 1f;

        private Slider _slider;
        private Coroutine _currentCoroutine;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }
    
        protected override void OnValueChanged(float current, float max)
        {
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
        
            _currentCoroutine = StartCoroutine(ChangeValueRoutine(current, max));
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