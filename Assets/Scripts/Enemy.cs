using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector2 NextWaypoint { get; set; }
    private float speed = 1.5f;
    private bool isMoving = false;

    private void OnEnable()
    {
        isMoving = true;
    }

    private void Update()
    {
        if(isMoving && NextWaypoint != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, NextWaypoint, Time.deltaTime * speed);
        }
    }
}
