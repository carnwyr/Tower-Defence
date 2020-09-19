using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : ITowerController
{
    public event Action<List<GameObject>, List<Vector2>> TowersPrepared;

    private readonly IObjectPooler _objectPooler;

    public TowerController(IObjectPooler objectPooler)
    {
        _objectPooler = objectPooler;
    }

    public void SetTowers(List<Vector2> towerPositions)
    {
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
}
