using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController
{
    private readonly ILevelSetter _levelSetter;
    private readonly IEnemyController _enemyController;
    private readonly IHealthController _healthController;

    private bool _gameStarted = false;

    public GameplayController(ILevelSetter levelSetter, IEnemyController enemyController, IHealthController healthController)
    {
        _levelSetter = levelSetter;
        _enemyController = enemyController;
        _healthController = healthController;
    }

    public void Start()
    {
        _levelSetter.DataLoaded += StartGame;
        _levelSetter.SetUpLevel(1);
    }

    public void Update()
    {
        if (!_gameStarted)
        {
            return;
        }

        _enemyController.Update();
    }

    private void StartGame(LevelData levelData)
    {
        _levelSetter.DataLoaded -= StartGame;
        _healthController.ResetHealth();
        _enemyController.BeginAttack();
        _gameStarted = true;
    }
}
