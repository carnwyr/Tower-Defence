using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action<int> DealDamage;
    public event Action<int> EnemyDied;

    public List<Vector2> Waypoints { get; set; }

    private float _attackCooldown = 2f;
    private int _damage = 5;
    private int _health = 10;
    private int _gold = 10;

    private int _currentWaypoint = 0;
    private float _speed = 3f;
    private bool _isMoving = false;

    private void OnEnable()
    {
        _currentWaypoint = 0;
        _isMoving = true;
        _health = 10;
    }

    private void OnDisable()
    {
        _isMoving = false;
        StopCoroutine(Attack());
    }

    private void Update()
    {
        if (_isMoving && _currentWaypoint < Waypoints.Count)
        {
            transform.position = Vector2.MoveTowards(transform.position, Waypoints[_currentWaypoint], Time.deltaTime * _speed);
            if (transform.position == new Vector3(Waypoints[_currentWaypoint].x, Waypoints[_currentWaypoint].y, transform.position.z))
            {
                if(++_currentWaypoint >= Waypoints.Count)
                {
                    _isMoving = false;
                    StartCoroutine(Attack());
                }
            }
        }
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            DealDamage?.Invoke(_damage);
            yield return new WaitForSeconds(_attackCooldown);
        }
    }

    public void GetAttacked(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            EnemyDied?.Invoke(_gold);
            gameObject.SetActive(false);
        }
    }
}
