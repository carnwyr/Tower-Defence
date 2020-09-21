using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class EnemyController : IEnemyController
{

    [System.Serializable]
    public class Config
    {
        public float waveDelay;
    }

    public event Action<List<GameObject>> NewWave;

    private readonly IObjectPooler _enemyPooler;

    private readonly int _enemyVariation = 3;
    private readonly int _damageGain = 3;
    private readonly int _healthGain = 4;
    private readonly int _goldGain = 5;

    private List<Vector2> _waypoints;
    private float _timeBetweenWaves = 10;

    private int _waveCount = 0;
    private float _timer = 0.0f;
    private int _enemiesKilled = 0;
    private bool _creatingWave = false;

    private int _baseDamage = 5;
    private int _baseHealth = 10;
    private int _baseGold = 10;

    private int _currentDamage = 5;
    private int _currentHealth = 10;
    private int _currentGold = 10;

    public EnemyController(IObjectPooler objectPooler, EnemyViewController enemyViewController)
    {
        _enemyPooler = objectPooler;
        _enemyPooler.NewObjectCreated += SubscribeToNewEnemies;

        enemyViewController.WaveEnded += ResumeWaveTimer;

        ReadConfig();
    }

    private void SubscribeToNewEnemies(GameObject pooledObj)
    {
        if (!pooledObj.CompareTag("Enemy"))
        {
            return;
        }
        var enemy = pooledObj.GetComponent<Enemy>();
        enemy.EnemyDied += IncreaseEnemyCount;
    }

    private void ResumeWaveTimer()
    {
        _creatingWave = false;
    }

    private void ReadConfig()
    {
        var path = Path.Combine(Application.dataPath, "config.json");
        if (!File.Exists(path))
        {
            string template = "{\n\t\"waveDelay\": 5\n}";
            File.WriteAllText(path, template, Encoding.UTF8);
        }
        var jsonString = File.ReadAllText(path);
        Config config = JsonUtility.FromJson<Config>(jsonString);
        _timeBetweenWaves = config.waveDelay;
    }

    private void IncreaseEnemyCount(int gold)
    {
        _enemiesKilled++;
    }

    public void SetWaypoints(List<Vector2> waypoints)
    {
        _waypoints = waypoints;
    }

    public void BeginAttack()
    {
        _timer = 0.0f;
        _waveCount = 0;
        _enemiesKilled = 0;
        _creatingWave = false;
        _currentDamage = _baseDamage;
        _currentHealth = _baseHealth;
        _currentGold = _baseGold;
        CreateNewWave();
    }

    public void Update()
    {
        if (_creatingWave)
        {
            return;
        }
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
        _creatingWave = true;
        var enemyCount = UnityEngine.Random.Range(_waveCount, _waveCount + _enemyVariation);
        var enemies = _enemyPooler.GetSeveral("Enemy", enemyCount);
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<Enemy>().Waypoints = _waypoints;
            enemy.GetComponent<Enemy>().SetStats(_currentDamage, _currentHealth, _currentGold);

        }
        NewWave?.Invoke(enemies);
        IncreaseCurrentStats();
    }

    private void IncreaseCurrentStats()
    {
        int damageIncrease = UnityEngine.Random.Range(0, 2);
        int healthIncrease = UnityEngine.Random.Range(0, 2);
        int goldIncrease = damageIncrease+ healthIncrease==0 ? 1 : UnityEngine.Random.Range(0, 2);

        _currentDamage += damageIncrease * _damageGain;
        _currentHealth += healthIncrease * _healthGain;
        _currentGold += goldIncrease * _goldGain;
    }

    public int GetEnemiesKilled()
    {
        return _enemiesKilled;
    }
}
