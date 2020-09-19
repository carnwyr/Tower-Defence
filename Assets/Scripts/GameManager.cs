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
    private GameObject _objectPoolerPrefab;

    private GameplayController _gameplayController;

    private void Awake()
    {
        var viewController = Instantiate(_viewControllerPrefab).GetComponent<ViewController>();
        var levelViewController = Instantiate(_levelViewControllerPrefab).GetComponent<LevelViewController>();
        levelViewController.Init(viewController.Canvas);
        var healthViewController = Instantiate(_healthViewControllerPrefab).GetComponent<HealthViewController>();
        healthViewController.Init(viewController.Canvas);
        var enemyViewController = new GameObject("EnemyVewController", typeof(EnemyViewController)).GetComponent<EnemyViewController>();

        SetDependencies(levelViewController, enemyViewController, healthViewController);
    }
    void Start()
    {
        _gameplayController.Start();
    }

    private void Update()
    {
        _gameplayController.Update();
    }

    private void SetDependencies(LevelViewController levelViewController, EnemyViewController enemyViewController, HealthViewController healthViewController)
    {
        var assetLoader = new AddressableAssetLoader<LevelData>();
        var levelSetter = new LevelSetter(assetLoader);
        var objectPooler = Instantiate(_objectPoolerPrefab).GetComponent<ObjectPooler>();
        var enemyController = new EnemyController(levelSetter, objectPooler);
        var healthController = new HealthController();
        _gameplayController = new GameplayController(levelSetter, enemyController, healthController);

        levelViewController.SetCallbacks(levelSetter);
        enemyViewController.SetCallbacks(levelSetter, enemyController);
        healthViewController.SetCallbacks(healthController);
    }
}
