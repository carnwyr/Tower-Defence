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
    private IInputController _inputController;

    private void Awake()
    {
        SetDependencies();
    }
    void Start()
    {
        _gameplayController.Start();
    }

    private void Update()
    {
        _inputController.Update();
        _gameplayController.Update();
    }

    private void SetDependencies()
    {
        var viewController = Instantiate(_viewControllerPrefab).GetComponent<ViewController>();
        var levelViewController = Instantiate(_levelViewControllerPrefab).GetComponent<LevelViewController>();
        var healthViewController = Instantiate(_healthViewControllerPrefab).GetComponent<HealthViewController>();
        var goldViewController = Instantiate(_goldViewControllerPrefab).GetComponent<GoldViewController>();
        var gameOverViewController = Instantiate(_gameOverViewControllerPrefab).GetComponent<GameOverViewController>();
        var enemyViewController = new GameObject("EnemyVewController", typeof(EnemyViewController)).GetComponent<EnemyViewController>();
        var towerViewController = new GameObject("TowerVewController", typeof(TowerViewController)).GetComponent<TowerViewController>();

        levelViewController.Init(viewController.CanvasBackground);
        healthViewController.Init(viewController.CanvasUI);
        goldViewController.Init(viewController.CanvasUI);
        gameOverViewController.Init(viewController.CanvasUI);

        var assetLoader = new AddressableAssetLoader<LevelData>();
        var levelSetter = new LevelSetter(assetLoader);
        var objectPooler = Instantiate(_objectPoolerPrefab).GetComponent<ObjectPooler>();
        var healthController = new HealthController(objectPooler);
        var goldController = new GoldController(objectPooler);
        var enemyController = new EnemyController(objectPooler, enemyViewController);
        var towerController = new TowerController(objectPooler, goldController);
        objectPooler.Init();

        _gameplayController = new GameplayController(levelSetter, enemyController, healthController, goldController, towerController, objectPooler);

        levelViewController.SetCallbacks(levelSetter);
        enemyViewController.SetCallbacks(levelSetter, enemyController, _gameplayController);
        healthViewController.SetCallbacks(healthController);
        goldViewController.SetCallbacks(goldController);
        gameOverViewController.SetCallbacks(_gameplayController);
        towerViewController.SetCallbacks(towerController);

        #if UNITY_ANDROID
                    _inputController = new TouchInputController();
        #else
                _inputController = new MouseInputController();
        #endif
    }
}
