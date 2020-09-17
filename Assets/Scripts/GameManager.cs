using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _sceneSetterObject;

    private GameplayController _gameplayController;

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
        _gameplayController.Update();
    }

    private void SetDependencies()
    {
        var levelLoader = new AddressableLevelLoader(_sceneSetterObject.GetComponent<SceneSetter>());
        _gameplayController = new GameplayController(levelLoader);
    }
}
