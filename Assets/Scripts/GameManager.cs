using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _viewControllerPrefab;
    [SerializeField]
    private GameObject _levelViewControllerPrefab;
    [SerializeField]
    private GameObject _healthViewControllerPrefab;
    [SerializeField]
    private GameObject _goldViewControllerPrefab;
    [SerializeField]
    private GameObject _gameOverViewControllerPrefab;
    [SerializeField]
    private GameObject _objectPoolerPrefab;

    private GameplayController _gameplayController;

    private void Awake()
    {
        var viewController = Instantiate(_viewControllerPrefab).GetComponent<ViewController>();
        var levelViewController = Instantiate(_levelViewControllerPrefab).GetComponent<LevelViewController>();
        levelViewController.Init(viewController.CanvasBackground);
        var healthViewController = Instantiate(_healthViewControllerPrefab).GetComponent<HealthViewController>();
        healthViewController.Init(viewController.CanvasUI);
        var goldViewController = Instantiate(_goldViewControllerPrefab).GetComponent<GoldViewController>();
        goldViewController.Init(viewController.CanvasUI);
        var gameOverViewController = Instantiate(_gameOverViewControllerPrefab).GetComponent<GameOverViewController>();
        gameOverViewController.Init(viewController.CanvasUI);
        var enemyViewController = new GameObject("EnemyVewController", typeof(EnemyViewController)).GetComponent<EnemyViewController>();
        var towerViewController = new GameObject("TowerVewController", typeof(TowerViewController)).GetComponent<TowerViewController>();

        SetDependencies(levelViewController, enemyViewController, healthViewController, goldViewController, gameOverViewController, towerViewController);
    }
    void Start()
    {
        _gameplayController.Start();
    }

    private void Update()
    {
        _gameplayController.Update();
    }

    private void SetDependencies(LevelViewController levelViewController, EnemyViewController enemyViewController, HealthViewController healthViewController, GoldViewController goldViewController, GameOverViewController gameOverViewController, TowerViewController towerViewController)
    {
        var assetLoader = new AddressableAssetLoader<LevelData>();
        var levelSetter = new LevelSetter(assetLoader);
        var objectPooler = Instantiate(_objectPoolerPrefab).GetComponent<ObjectPooler>();
        var healthController = new HealthController(objectPooler);
        var goldController = new GoldController(objectPooler);
        objectPooler.Init();
        var enemyController = new EnemyController(objectPooler);
        var towerController = new TowerController(objectPooler);
        _gameplayController = new GameplayController(levelSetter, enemyController, healthController, goldController, towerController, objectPooler);

        levelViewController.SetCallbacks(levelSetter);
        enemyViewController.SetCallbacks(levelSetter, enemyController);
        healthViewController.SetCallbacks(healthController);
        goldViewController.SetCallbacks(goldController);
        gameOverViewController.SetCallbacks(_gameplayController);
        towerViewController.SetCallbacks(towerController);
    }
}
