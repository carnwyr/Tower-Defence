using System;
using UnityEngine.EventSystems;

public class GameplayController
{
    public Action GameStarted;
    public Action<int> GameEnded;

    private readonly ILevelSetter _levelSetter;
    private readonly IEnemyController _enemyController;
    private readonly IHealthController _healthController;
    private readonly IGoldController _goldController;
    private readonly ITowerController _towerController;
    private readonly IObjectPooler _objectPooler;

    private bool _gameStarted = false;

    public GameplayController(ILevelSetter levelSetter, IEnemyController enemyController, IHealthController healthController, IGoldController goldController, ITowerController towerController, IObjectPooler objectPooler)
    {
        _levelSetter = levelSetter;
        _enemyController = enemyController;
        _healthController = healthController;
        _goldController = goldController;
        _towerController = towerController;
        _objectPooler = objectPooler;

        _healthController.HealthIsZero += EndGame;
    }

    ~GameplayController()
    {
        _healthController.HealthIsZero -= EndGame;
    }

    public void Start()
    {
        _levelSetter.DataLoaded += PrepareGame;
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

    private void PrepareGame(LevelData levelData)
    {
        _levelSetter.DataLoaded -= PrepareGame;
        _towerController.SetTowers(levelData.TowerPositions);
        _enemyController.SetWaypoints(levelData.WaypointPositions);
        StartGame();
    }

    public void StartGame()
    {
        _towerController.ResetTowers();
        _healthController.ResetHealth();
        _goldController.ResetGold();
        _enemyController.BeginAttack();
        _gameStarted = true;
        GameStarted?.Invoke();
    }

    private void EndGame()
    {
        _gameStarted = false;
        _objectPooler.HideByTag("Enemy");
        _objectPooler.HideByTag("Bullet");
        var enemiesKilled = _enemyController.GetEnemiesKilled();
        GameEnded?.Invoke(enemiesKilled);
    }
}
