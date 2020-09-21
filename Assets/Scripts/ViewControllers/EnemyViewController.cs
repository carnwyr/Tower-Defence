using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyViewController : MonoBehaviour
{
    public event Action WaveEnded;

    private Vector2 _spawnPosition;
    private Coroutine _coroutine;

    public void SetCallbacks(ILevelSetter levelSetter, IEnemyController enemyController, GameplayController gameplayController)
    {
        levelSetter.DataLoaded += SetSpawnPosition;
        enemyController.NewWave += SpawnWave;
        gameplayController.GameEnded += StopSpawning;
    }

    private void SetSpawnPosition(LevelData levelData)
    {
        _spawnPosition = levelData.SpawnPosition;
    }

    private void SpawnWave(List<GameObject> enemies)
    {
        _coroutine = StartCoroutine(SpawnEnemies(enemies));
    }

    private IEnumerator SpawnEnemies(List<GameObject> enemies)
    {
        foreach (var enemy in enemies)
        {
            SpawnEnemy(enemy);
            yield return new WaitForSeconds(1f);
        }
        WaveEnded?.Invoke();
    }

    private void SpawnEnemy(GameObject enemy)
    {
        enemy.transform.position = _spawnPosition;
        enemy.SetActive(true);
    }
    private void StopSpawning(int enemies)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            WaveEnded?.Invoke();
        }
    }
}
