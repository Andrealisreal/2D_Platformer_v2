using UnityEngine;

public class Health
{
    private readonly float _maxHealth;
    private float _currentHealth;
    private Animator _animator;
    
    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;
    
    public Health(float currentHealth, Animator animator)
    {
        _currentHealth = currentHealth;
        _maxHealth = currentHealth;
        _animator = animator;
    }

    public void Heal(float amount)
    {
        if (_currentHealth + amount >= _maxHealth)
        {
            _currentHealth = _maxHealth;
            Debug.Log($"Здоровье максимальное - {_currentHealth}");
            
            return;
        }
        
        _currentHealth += amount;
        Debug.Log($"Была получена аптечка - {amount}. Текущие здоровье - {_currentHealth}");
    }

    public void TakeDamage(float amount)
    {
        if (_currentHealth - amount <= 0)
        {
            _currentHealth = 0;
            
            return;
        }
        
        _currentHealth -= amount;
        Debug.Log($"Был получен урон - {amount}. Текущие здоровье - {_currentHealth}");
        Debug.Log($"Урон получил - {_animator.gameObject.name}");
    }
}
