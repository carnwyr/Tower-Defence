using System;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyController
{
    event Action<List<GameObject>> NewWave;
    void BeginAttack();
    void Update();
    void SetWaypoints(List<Vector2> waypoints);
    int GetEnemiesKilled();
}
