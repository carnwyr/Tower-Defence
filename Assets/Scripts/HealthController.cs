using System;
using UnityEngine;

public class HealthController : IHealthController
{
    public event Action<int> HealthChanged;
    public event Action HealthIsZero;

    private Action _unsubscribe;

    private int _health;

    public HealthController(IObjectPooler enemyPooler)
    {
        enemyPooler.NewObjectCreated += SubscribeToNewEnemies;

        _unsubscribe = () => RemoveCallbacks(enemyPooler);
    }

    ~HealthController()
    {
        _unsubscribe();
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

    private void RemoveCallbacks(IObjectPooler enemyPooler)
    {
        enemyPooler.NewObjectCreated -= SubscribeToNewEnemies;

        var enemies = enemyPooler.GetFullList("Enemy");
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<Enemy>().DealDamage -= ReduceHealth;
        }
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
