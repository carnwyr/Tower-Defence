using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController
{
    private readonly ILevelSetter _levelSetter;
    private readonly IEnemyController _enemyController;

    private bool _gameStarted = false;
    private int _health = 100;

    public GameplayController(ILevelSetter levelSetter, IEnemyController enemyController)
    {
        _levelSetter = levelSetter;
        _enemyController = enemyController;
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
        _health = 100;
        _enemyController.BeginAttack();
        _gameStarted = true;
    }
}
