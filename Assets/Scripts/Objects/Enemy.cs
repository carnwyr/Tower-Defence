using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<Vector2> Waypoints { get; set; }

    private int _currentWaypoint = 0;
    private float _speed = 3f;
    private bool _isMoving = false;

    private void OnEnable()
    {
        _isMoving = true;
    }

    private void Update()
    {
        if(_isMoving && _currentWaypoint < Waypoints.Count)
        {
            transform.position = Vector2.MoveTowards(transform.position, Waypoints[_currentWaypoint], Time.deltaTime * _speed);
            if (transform.position == new Vector3(Waypoints[_currentWaypoint].x, Waypoints[_currentWaypoint].y, 0))
            {
                if(++_currentWaypoint >= Waypoints.Count)
                {
                    _isMoving = false;
                }
            }
        }
    }
}
