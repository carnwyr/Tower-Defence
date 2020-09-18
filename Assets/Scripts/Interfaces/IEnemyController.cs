using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyController
{
    event Action<List<GameObject>> NewWave;
    void BeginAttack();
    void Update();
}
