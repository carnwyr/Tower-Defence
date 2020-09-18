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

    public void SetCallbacks(ILevelSetter levelSetter, IEnemyController enemyController)
    {
        levelSetter.DataLoaded += SetSpawnPosition;

        enemyController.NewWave += SpawnWave;

        _unsubscribe = () => RemoveCallbacks(levelSetter, enemyController);
    }

    private void RemoveCallbacks(ILevelSetter levelSetter, IEnemyController enemyController)
    {
        if (levelSetter != null)
        {
            levelSetter.DataLoaded -= SetSpawnPosition;
        }
        if (enemyController != null)
        {
            enemyController.NewWave -= SpawnWave;
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
