using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _viewControllerPrefab;
    [SerializeField]
    private GameObject _objectPoolerPrefab;

    private GameplayController _gameplayController;

    private void Awake()
    {
        var levelViewController = Instantiate(_viewControllerPrefab).GetComponent<LevelViewController>();
        var enemyViewController = new GameObject("EnemyVewController", typeof(EnemyViewController)).GetComponent<EnemyViewController>();

        SetDependencies(levelViewController, enemyViewController);
    }
    void Start()
    {
        _gameplayController.Start();
    }

    private void Update()
    {
        _gameplayController.Update();
    }

    private void SetDependencies(LevelViewController levelViewController, EnemyViewController enemyViewController)
    {
        var assetLoader = new AddressableAssetLoader<LevelData>();
        var levelSetter = new LevelSetter(assetLoader);
        var objectPooler = Instantiate(_objectPoolerPrefab).GetComponent<ObjectPooler>();
        _gameplayController = new GameplayController(levelSetter, objectPooler);

        levelViewController.SetCallbacks(levelSetter);
        enemyViewController.SetCallbacks(levelSetter, _gameplayController);
    }
}
