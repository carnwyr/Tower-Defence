using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverViewController : MonoBehaviour
{
    [SerializeField]
    private GameObject _menuPrefab;
    [SerializeField]
    private GameObject _enemiesTextPrefab;
    [SerializeField]
    private GameObject _restartButtonPrefab;

    private GameObject _menu;
    private GameObject _enemiesText;
    private GameObject _restartButton;

    public void Init(GameObject canvas)
    {
        _menu = Instantiate(_menuPrefab);
        _menu.transform.SetParent(canvas.transform, false);
        _enemiesText = Instantiate(_enemiesTextPrefab);
        _enemiesText.transform.SetParent(_menu.transform, false);
        _restartButton = Instantiate(_restartButtonPrefab);
        _restartButton.transform.SetParent(_menu.transform, false);
        _menu.SetActive(false);
    }

    public void SetCallbacks(GameplayController gameplayController)
    {
        gameplayController.GameEnded += DisplayGameOver;
        gameplayController.GameStarted += HideGameOver;

        _restartButton.GetComponent<Button>().onClick.AddListener(gameplayController.StartGame);
    }

    private void DisplayGameOver(int enemiesCount)
    {
        var enemiesText = _enemiesText.GetComponent<Text>();
        enemiesText.text = "Enemies killed: " + enemiesCount.ToString();
        _menu.SetActive(true);
    }

    private void HideGameOver()
    {
        _menu.SetActive(false);
    }
}
