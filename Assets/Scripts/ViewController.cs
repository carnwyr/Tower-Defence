using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ViewController : MonoBehaviour
{
    [SerializeField]
    private GameObject _cameraPrefab;
    [SerializeField]
    private GameObject _canvasPrefab;
    [SerializeField]
    private GameObject _eventSystemPrefab;
    [SerializeField]
    private GameObject _backgroundPrefab;
    [SerializeField]
    private GameObject _towerPrefab;
    [SerializeField]
    private GameObject _waypointPrefab;

    private Action _unsubscribe;
    private Image _background;
    private Vector2 _spawnPosition;
    private GameObject _firstWaypoint;

    private void Awake()
    {
        Instantiate(_eventSystemPrefab);
        var camera = Instantiate(_cameraPrefab);
        var canvas = Instantiate(_canvasPrefab);
        var background = Instantiate(_backgroundPrefab);
        canvas.GetComponent<Canvas>().worldCamera = camera.GetComponent<Camera>();
        background.transform.SetParent(canvas.transform, false);
        
        _background = background.GetComponent<Image>();
    }

    private void OnDestroy()
    {
        _unsubscribe();
    }

    public void SetCallbacks(ILevelSetter levelSetter, GameplayController gameplayController)
    {
        levelSetter.DataLoaded += SetBackground;
        levelSetter.DataLoaded += SetTowers; 
        levelSetter.DataLoaded += SetWaypoints;
        levelSetter.DataLoaded += SetSpawnPosition;

        gameplayController.NewWave += SpawnWave;

        _unsubscribe = () => RemoveCallbacks(levelSetter, gameplayController);
    }

    private void RemoveCallbacks(ILevelSetter levelSetter, GameplayController gameplayController)
    {
        if (levelSetter != null)
        {
            levelSetter.DataLoaded -= SetBackground;
            levelSetter.DataLoaded -= SetTowers;
            levelSetter.DataLoaded -= SetWaypoints;
            levelSetter.DataLoaded -= SetSpawnPosition;
        }
        if (gameplayController != null)
        {
            gameplayController.NewWave -= SpawnWave;
        }
    }

    private void SetBackground(LevelData levelData)
    {
        var sprite = levelData.Background;
        _background.sprite = sprite;
        _background.preserveAspect = true;
        _background.gameObject.GetComponent<AspectRatioFitter>().aspectRatio = sprite.bounds.size.x / sprite.bounds.size.y;
    }

    private void SetTowers(LevelData levelData)
    {
        var parent =new GameObject("Towers");
        
        foreach(var pos in levelData.TowerPositions)
        {
            var tower = Instantiate(_towerPrefab).transform;
            tower.position = pos;
            tower.SetParent(parent.transform);
        }
    }

    private void SetWaypoints(LevelData levelData)
    {
        var parent = new GameObject("Waypoints");
        GameObject previousWaypoint = null;

        foreach (var pos in levelData.WaypointPositions)
        {
            var waypoint = Instantiate(_waypointPrefab);
            waypoint.transform.position = pos;
            waypoint.transform.SetParent(parent.transform);
            if (previousWaypoint == null)
            {
                _firstWaypoint = waypoint;
            } else
            {
                previousWaypoint.GetComponent<Waypoint>().NextWaypoint = waypoint;
            }
            previousWaypoint = waypoint;
        }
    }

    private void SetSpawnPosition(LevelData levelData)
    {
        _spawnPosition = levelData.SpawnPosition;
    }

    private void SpawnWave(int enemiesCount, IObjectPooler enemyPool)
    {
        StartCoroutine(SpawnEnemies(enemiesCount, enemyPool));
    }

    private IEnumerator SpawnEnemies(int enemiesCount, IObjectPooler enemyPool)
    {
        var count = 0;
        while (count < enemiesCount)
        {
            count++;
            SpawnEnemy(enemyPool.GetPooledObject("Enemy"));
            yield return new WaitForSeconds(1f);
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        enemy.GetComponent<Enemy>().NextWaypoint = _firstWaypoint;
        enemy.transform.position = _spawnPosition;
        enemy.SetActive(true);
    }
}
