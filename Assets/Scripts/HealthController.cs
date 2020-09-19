using System;

public class HealthController : IHealthController
{
    public event Action<int> HealthChanged;
    public event Action HealthIsZero;

    private int _health;

    public void ResetHealth()
    {
        _health = 100;
        HealthChanged?.Invoke(_health);
    }

    public void ReduceHealth(int damage)
    {
        _health = Math.Max(0, _health - damage);
        HealthChanged?.Invoke(_health);
        if (_health == 0)
        {
            HealthIsZero?.Invoke();
        }
    }
}
