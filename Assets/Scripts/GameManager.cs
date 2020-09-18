using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _viewControllerPrefab;

    private GameplayController _gameplayController;

    private void Awake()
    {
        var viewController = Instantiate(_viewControllerPrefab).GetComponent<ViewController>();

        SetDependencies(viewController);
    }
    void Start()
    {
        _gameplayController.Start();

    }

    private void Update()
    {
        _gameplayController.Update();
    }

    private void SetDependencies(ViewController viewController)
    {
        var assetLoader = new AddressableAssetLoader<LevelData>();
        var levelSetter = new LevelSetter(assetLoader);
        _gameplayController = new GameplayController(levelSetter);

        viewController.SetCallbacks(levelSetter);
    }
}
