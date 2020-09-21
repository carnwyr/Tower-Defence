using System;
using UnityEngine;

public class HealthController : IHealthController
{
    public event Action<int> HealthChanged;
    public event Action HealthIsZero;

    private int _health;

    public HealthController(IObjectPooler enemyPooler)
    {
        enemyPooler.NewObjectCreated += SubscribeToNewEnemies;
    }

    private void SubscribeToNewEnemies(GameObject pooledObj)
    {
        if (!pooledObj.CompareTag("Enemy"))
        {
            return;
        }
        var enemy = pooledObj.GetComponent<Enemy>();
        enemy.DealDamage += ReduceHealth;
    }

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
