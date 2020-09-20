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

    private GameObject _menu;
    private GameObject _enemiesText;
    private Action _unsubscribe;

    public void Init(GameObject canvas)
    {
        _menu = Instantiate(_menuPrefab);
        _menu.transform.SetParent(canvas.transform, false);
        _enemiesText = Instantiate(_enemiesTextPrefab);
        _enemiesText.transform.SetParent(_menu.transform, false);
        _menu.SetActive(false);
    }

    private void OnDestroy()
    {
        _unsubscribe();
    }

    public void SetCallbacks(GameplayController gameplayController)
    {
        gameplayController.GameEnded += DisplayGameOver;
        gameplayController.GameStarted += HideGameOver;

        _unsubscribe = () => RemoveCallbacks(gameplayController);
    }

    private void RemoveCallbacks(GameplayController gameplayController)
    {
        if (gameplayController != null)
        {
            gameplayController.GameEnded -= DisplayGameOver;
            gameplayController.GameStarted -= HideGameOver;
        }
    }

    private void DisplayGameOver(int enemiesCount)
    {
        var enemiesText = _enemiesText.GetComponent<Text>();
        enemiesText.text = "Killed enemies: " + enemiesCount.ToString();
        _menu.SetActive(true);
    }

    private void HideGameOver()
    {
        _menu.SetActive(false);
    }
}
