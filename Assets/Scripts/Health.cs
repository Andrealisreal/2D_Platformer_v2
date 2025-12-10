public class Health
{
    private readonly float _maxHealth;
    private float _currentHealth;
    
    public Health(float currentHealth, float maxHealth)
    {
        _currentHealth = currentHealth;
        _maxHealth = maxHealth;
    }

    public void Heal(float amount)
    {
        if (_currentHealth + amount >= _maxHealth)
        {
            _currentHealth = _maxHealth;

            return;
        }
        
        _currentHealth += amount;
    }

    public void TakeDamage(float amount)
    {
        if (_currentHealth - amount <= 0)
        {
            _currentHealth = 0;
            
            return;
        }
        
        _currentHealth -= amount;
    }
}
