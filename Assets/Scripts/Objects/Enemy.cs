using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject NextWaypoint { get; set; }

    private float _speed = 3f;
    private bool _isMoving = false;

    private void OnEnable()
    {
        _isMoving = true;
    }

    private void Update()
    {
        if(_isMoving && NextWaypoint != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, NextWaypoint.transform.position, Time.deltaTime * _speed);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Waypoint") && _isMoving)
        {
            if (transform.position == NextWaypoint.transform.position)
            {
                var nextWaypoint = collider.GetComponent<Waypoint>().NextWaypoint;
                NextWaypoint = nextWaypoint;
                if (nextWaypoint == null)
                {
                    _isMoving = false;
                }
            }
        }
    }
}
