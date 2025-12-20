using UnityEngine;

namespace UI
{
    public abstract class HealthView : MonoBehaviour
    {
        private Health _health;

        private void OnDestroy()
        {
            _health.Changed -= OnValueChanged;
        }
    
        public void Initialize(Health health)
        {
            _health = health;
            _health.Changed += OnValueChanged;
            OnValueChanged(_health.Current, _health.Max);
        }

        protected abstract void OnValueChanged(float current, float max);
    }
}
