using System;
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

    private void Awake()
    {
        Instantiate(_eventSystemPrefab);
        var camera = Instantiate(_cameraPrefab);
        var canvas = Instantiate(_canvasPrefab);
        var background = Instantiate(_backgroundPrefab);
        canvas.GetComponent<Canvas>().worldCamera = camera.GetComponent<Camera>();
        background.transform.SetParent(canvas.transform);
        
        _background = background.GetComponent<Image>();
    }

    private void OnDestroy()
    {
        _unsubscribe();
    }

    public void SetCallbacks(ILevelSetter levelSetter)
    {
        levelSetter.DataLoaded += SetBackground;
        levelSetter.DataLoaded += SetTowers; 
        levelSetter.DataLoaded += SetWaypoints;

        _unsubscribe = () => RemoveCallbacks(levelSetter);
    }

    private void RemoveCallbacks(ILevelSetter levelSetter)
    {
        if (levelSetter != null)
        {
            levelSetter.DataLoaded -= SetBackground;
            levelSetter.DataLoaded -= SetTowers;
            levelSetter.DataLoaded -= SetWaypoints;
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
            tower.transform.position = pos;
            tower.transform.SetParent(parent.transform);
        }
    }

    private void SetWaypoints(LevelData levelData)
    {
        var parent = new GameObject("Waypoints");

        foreach (var pos in levelData.WaypointPositions)
        {
            var waypoint = Instantiate(_waypointPrefab).transform;
            waypoint.transform.position = pos;
            waypoint.transform.SetParent(parent.transform);
        }
    }
}
