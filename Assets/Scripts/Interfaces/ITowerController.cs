using System;
using System.Collections.Generic;
using UnityEngine;

public interface ITowerController
{
    event Action<List<GameObject>, List<Vector2>> TowersPrepared;
    void SetTowers(List<Vector2> towerPositions);
    void ResetTowers();
}
