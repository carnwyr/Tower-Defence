public class GameplayController
{
    private readonly ILevelSetter _levelSetter;
    private readonly IEnemyController _enemyController;
    private readonly IHealthController _healthController;
    private readonly IGoldController _goldController;
    private readonly ITowerController _towerController;

    private bool _gameStarted = false;

    public GameplayController(ILevelSetter levelSetter, IEnemyController enemyController, IHealthController healthController, IGoldController goldController, ITowerController towerController)
    {
        _levelSetter = levelSetter;
        _enemyController = enemyController;
        _healthController = healthController;
        _goldController = goldController;
        _towerController = towerController;
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
        _towerController.SetTowers(levelData.TowerPositions);
        _healthController.ResetHealth();
        _goldController.ResetGold();
        _enemyController.SetWaypoints(levelData.WaypointPositions);
        _enemyController.BeginAttack();
        _gameStarted = true;
    }
}
