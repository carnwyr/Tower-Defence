using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : ITowerController
{
    public event Action<List<GameObject>, List<Vector2>> TowersPrepared;

    private Action _unsubscribe;

    private readonly IObjectPooler _objectPooler;
    private readonly IGoldController _goldController;

    private readonly int _baseDamage = 5;
    private readonly float _baseAttackCooldown = 2f;
    private readonly int _levelUpCost = 50;

    public TowerController(IObjectPooler objectPooler, IGoldController goldController)
    {
        _goldController = goldController;
        _objectPooler = objectPooler;
        _objectPooler.NewObjectCreated += SubscribeToNewTowers;

        _unsubscribe = () => RemoveCallbacks();
    }

    ~TowerController()
    {
        _unsubscribe();
    }

    private void SubscribeToNewTowers(GameObject pooledObj)
    {
        if (!pooledObj.CompareTag("Tower"))
        {
            return;
        }
        var tower = pooledObj.GetComponent<Tower>();
        tower.TowerLevelUp += HandleLevelUp;
    }

    private void RemoveCallbacks()
    {
        _objectPooler.NewObjectCreated -= SubscribeToNewTowers;

        var towers = _objectPooler.GetFullList("Tower");
        foreach (var tower in towers)
        {
            tower.GetComponent<Tower>().TowerLevelUp -= HandleLevelUp;
        }
    }

    private void HandleLevelUp(Tower tower)
    {
        if (_goldController.GetGold() >= _levelUpCost)
        {
            _goldController.ChangeGold(-_levelUpCost);
            tower.LevelUpStats();
        }
    }

    public void SetTowers(List<Vector2> towerPositions)
    {
        _objectPooler.HideByTag("Tower");
        var towers = _objectPooler.GetSeveral("Tower", towerPositions.Count);
        foreach (var tower in towers)
        {
            tower.GetComponent<Tower>().ShootBullet += ShootBullet;
        }
        TowersPrepared?.Invoke(towers, towerPositions);
    }

    private void ShootBullet(GameObject tower, GameObject enemy)
    {
        var bullet = _objectPooler.GetPooledObject("Bullet");
        bullet.transform.position = tower.transform.position;
        bullet.SetActive(true);
        var damage = tower.GetComponent<Tower>().GetDamage();
        bullet.GetComponent<Bullet>().Shoot(enemy, damage);
    }

    public void ResetTowers()
    {
        var towers = _objectPooler.GetFullList("Tower");
        foreach(var tower in towers)
        {
            tower.GetComponent<Tower>().SetStats(_baseDamage, _baseAttackCooldown);
        }
    }
}
