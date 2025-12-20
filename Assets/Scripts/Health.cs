using System;
using UnityEngine;

public class Health
{
    public event Action Died;
    public event Action<float, float> Changed;
    
    public Health(float max)
    {
        Current = max;
        Max = max;
    }
    
    public float Current { get; private set; }

    public float Max { get; }

    public void Heal(float amount)
    {
        Current = Mathf.Min(Current + amount, Max);
        Changed?.Invoke(Current, Max);
    }

    public void TakeDamage(float amount)
    {
        Current = Mathf.Max(Current - amount, 0);
        Changed?.Invoke(Current, Max);
        IsDead();
    }
    
    private void IsDead()
    {
        if (Current <= 0)
            Died?.Invoke();
    }
}
