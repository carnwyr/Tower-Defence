using System;

public interface IHealthController
{
    event Action<int> HealthChanged;
    event Action HealthIsZero;

    void ResetHealth();
    void ReduceHealth(int damage);
}
