using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LevelData", menuName = "Level Data", order = 51)]
public class LevelData : ScriptableObject
{
    [SerializeField]
    private string levelName;
    [SerializeField]
    private Sprite background;
    [SerializeField]
    private List<Vector2> towerPositions;
    [SerializeField]
    private Vector2 spawnPosition;
    [SerializeField]
    private List<Vector2> waypointPositions;

    public string LevelName => levelName;
    public Sprite Background => background;
    public List<Vector2> TowerPositions => towerPositions;
    public Vector2 SpawnPosition => spawnPosition;
    public List<Vector2> WaypointPositions => waypointPositions;
}