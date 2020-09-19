using System;
using UnityEngine;

public class GoldController : IGoldController
{
    public event Action<int> GoldChanged;

    private Action _unsubscribe;

    private int _gold;

    public GoldController(IObjectPooler enemyPooler)
    {
        enemyPooler.NewObjectCreated += SubscribeToNewEnemies;

        _unsubscribe = () => RemoveCallbacks(enemyPooler);
    }

    ~GoldController()
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
        enemy.EnemyDied += ChangeGold;
    }

    private void RemoveCallbacks(IObjectPooler enemyPooler)
    {
        enemyPooler.NewObjectCreated -= SubscribeToNewEnemies;

        var enemies = enemyPooler.GetFullList("Enemy");
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<Enemy>().EnemyDied -= ChangeGold;
        }
    }

    public void ResetGold()
    {
        _gold = 0;
        GoldChanged?.Invoke(_gold);
    }

    public void ChangeGold(int amount)
    {
        _gold += amount;
        GoldChanged?.Invoke(_gold);
    }
}
