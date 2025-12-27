using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Players.Abilities.Vampirisms
{
    [RequireComponent(typeof(Slider))]
    public class VampirismView : MonoBehaviour
    {
        [SerializeField] private Vampirism _vampirism;
        [SerializeField] private SpriteRenderer _auraRenderer;
        [SerializeField] private Transform _target;

        private Slider _slider;
        private Coroutine _currentCoroutine;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void Start()
        {
            _auraRenderer.enabled = false;
            SetAura(_vampirism.Radius);
        }

        private void OnEnable()
        {
            _vampirism.Activated += OnValueChanged;
            _vampirism.Deactivated += OnValueChanged;
        }

        private void OnDisable()
        {
            _vampirism.Activated -= OnValueChanged;
            _vampirism.Deactivated -= OnValueChanged;
        }

        private void SetAura(float radius)
        {
            Vector2 spriteSize = _auraRenderer.sprite.bounds.size;
            var targetDiameter = radius * 2f;
            var scale = targetDiameter / spriteSize.x;

            _auraRenderer.transform.localScale = new Vector3(scale, scale, 1f);
        }

        private void OnValueChanged(float amount)
        {
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);

            _currentCoroutine = StartCoroutine(ChangeValueRoutine(amount));
        }

        private IEnumerator ChangeValueRoutine(float amount)
        {
            var isActivated = _slider.value > 0.9f;
            
            var startValue = _slider.value;
            var targetValue = isActivated ? 0f : 1f;
            var distance = Mathf.Abs(targetValue - startValue);
            var speed = distance / amount;
            
            if (isActivated)
            {
                _auraRenderer.enabled = true;
                
                while (_slider.value > _slider.minValue)
                {
                    var moveAmount = speed * Time.deltaTime;

                    _slider.value = Mathf.MoveTowards(_slider.value, targetValue, moveAmount);
                    _auraRenderer.transform.position = _target.position;

                    yield return null;
                }

                _auraRenderer.enabled = false;
            }
            else
            {
                while (_slider.value < _slider.maxValue)
                {
                    var moveAmount = speed * Time.deltaTime;

                    _slider.value = Mathf.MoveTowards(_slider.value, targetValue, moveAmount);

                    yield return null;
                }
            }
        }
    }
}