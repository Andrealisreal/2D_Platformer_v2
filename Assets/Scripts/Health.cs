using System;
using UnityEngine;

public class Health
{
    public event Action OnDeath;

    public float CurrentHealth { get; private set; }

    public float MaxHealth { get; }

    public Health(float currentHealth)
    {
        CurrentHealth = currentHealth;
        MaxHealth = currentHealth;
    }

    public void Heal(float amount)
    {
        if (CurrentHealth + amount >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
            Debug.Log($"Здоровье максимальное - {CurrentHealth}");
            
            return;
        }
        
        CurrentHealth += amount;
        Debug.Log($"Была получена аптечка - {amount}. Текущие здоровье - {CurrentHealth}");
    }

    public void TakeDamage(float amount)
    {
        if (CurrentHealth - amount <= 0)
        {
            CurrentHealth = 0;
            IsDead();
            
            return;
        }
        
        CurrentHealth -= amount;
        Debug.Log($"Был получен урон - {amount}. Текущие здоровье - {CurrentHealth}");
    }
    
    private void IsDead()
    {
        if (CurrentHealth <= 0)
            OnDeath?.Invoke();
    }
}
