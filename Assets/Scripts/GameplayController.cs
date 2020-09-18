using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController
{
    public Action<int, IObjectPooler> NewWave;

    private readonly ILevelSetter _levelsetter;
    private readonly IObjectPooler _enemyPooler;

    private readonly float _timeBetweenWaves = 10;
    private readonly int _enemyVariation = 3;

    private float _timer = 0.0f;
    private int _waveCount = 0;
    private bool _gameStarted = false;
    private GameObject firstWaypoint;

    public GameplayController(ILevelSetter levelsetter, IObjectPooler objectPooler)
    {
        _levelsetter = levelsetter;
        _enemyPooler = objectPooler;
    }

    public void Start()
    {
        _levelsetter.DataLoaded += StartGame;
        _levelsetter.SetUpLevel(1);
    }

    public void Update()
    {
        if (!_gameStarted)
        {
            return;
        }

        _timer += Time.deltaTime;
        if (_timer > _timeBetweenWaves)
        {
            _timer -= _timeBetweenWaves;
            StartNewWave();
        }
    }

    private void StartGame(LevelData levelData)
    {
        _levelsetter.DataLoaded -= StartGame;
        _waveCount = 0;
        _timer = 0.0f;
        StartNewWave();
        _gameStarted = true;
    }

    private void StartNewWave()
    {
        _waveCount++;
        var enemyCount = UnityEngine.Random.Range(_waveCount, _waveCount + _enemyVariation);
        NewWave?.Invoke(enemyCount, _enemyPooler);
    }
}
