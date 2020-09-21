using System;
using UnityEngine;

public class GoldController : IGoldController
{
    public event Action<int> GoldChanged;

    public int _gold;

    public GoldController(IObjectPooler enemyPooler)
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
        enemy.EnemyDied += ChangeGold;
    }

    public int GetGold()
    {
        return _gold;
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
