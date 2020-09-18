using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyViewController : MonoBehaviour
{
    private Action _unsubscribe;
    private Vector2 _spawnPosition;

    private void OnDestroy()
    {
        _unsubscribe();
    }

    public void SetCallbacks(ILevelSetter levelSetter, GameplayController gameplayController)
    {
        levelSetter.DataLoaded += SetSpawnPosition;

        gameplayController.NewWave += SpawnWave;

        _unsubscribe = () => RemoveCallbacks(levelSetter, gameplayController);
    }

    private void RemoveCallbacks(ILevelSetter levelSetter, GameplayController gameplayController)
    {
        if (levelSetter != null)
        {
            levelSetter.DataLoaded -= SetSpawnPosition;
        }
        if (gameplayController != null)
        {
            gameplayController.NewWave -= SpawnWave;
        }
    }

    private void SetSpawnPosition(LevelData levelData)
    {
        _spawnPosition = levelData.SpawnPosition;
    }

    private void SpawnWave(List<GameObject> enemies)
    {
        StartCoroutine(SpawnEnemies(enemies));
    }

    private IEnumerator SpawnEnemies(List<GameObject> enemies)
    {
        foreach (var enemy in enemies)
        {
            SpawnEnemy(enemy);
            yield return new WaitForSeconds(1f);
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        enemy.transform.position = _spawnPosition;
        enemy.SetActive(true);
    }
}
