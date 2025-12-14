using System;

public class Health
{
    public event Action Died;
    
    public Health(float currentHealth)
    {
        CurrentHealth = currentHealth;
        MaxHealth = currentHealth;
    }
    
    public float CurrentHealth { get; private set; }

    public float MaxHealth { get; }

    public void Heal(float amount)
    {
        if (CurrentHealth + amount >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
            
            return;
        }
        
        CurrentHealth += amount;
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
    }
    
    private void IsDead()
    {
        if (CurrentHealth <= 0)
            Died?.Invoke();
    }
}
