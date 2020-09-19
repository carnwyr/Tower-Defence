using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : IEnemyController
{
    public event Action<List<GameObject>> NewWave;
    private Action _unsubscribe;

    private readonly IObjectPooler _enemyPooler;

    private readonly float _timeBetweenWaves = 10;
    private readonly int _enemyVariation = 3;

    private List<Vector2> _waypoints;

    private int _waveCount = 0;
    private float _timer = 0.0f;

    public EnemyController(ILevelSetter levelSetter, IObjectPooler objectPooler)
    {
        _enemyPooler = objectPooler;
        levelSetter.DataLoaded += GetWaypoints;

        _unsubscribe = () => RemoveCallbacks(levelSetter);
    }

    ~EnemyController()
    {
        _unsubscribe();
    }

    public void GetWaypoints(LevelData levelData)
    {
        _waypoints = levelData.WaypointPositions;
    }

    private void RemoveCallbacks(ILevelSetter levelSetter)
    {
        if (levelSetter != null)
        {
            levelSetter.DataLoaded -= GetWaypoints;
        }
    }

    public void BeginAttack()
    {
        _timer = 0.0f;
        _waveCount = 0;
        CreateNewWave();
    }

    public void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _timeBetweenWaves)
        {
            _timer -= _timeBetweenWaves;
            CreateNewWave();
        }
    }

    private void CreateNewWave()
    {
        _waveCount++;
        var enemyCount = UnityEngine.Random.Range(_waveCount, _waveCount + _enemyVariation);
        var enemies = _enemyPooler.GetSeveral("Enemy", enemyCount);
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<Enemy>().Waypoints = _waypoints;
        }
        NewWave?.Invoke(enemies);
    }
}
