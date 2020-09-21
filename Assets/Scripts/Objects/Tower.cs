using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public event Action<GameObject, GameObject> ShootBullet;
    public event Action<Tower> TowerLevelUp;

    private readonly int _damageGain = 3;
    private readonly float _cooldownDecrease = 0.2f;

    private int _damage = 5;
    private float _attackCooldown = 2f;

    private List<GameObject> _accessibleEnemies = new List<GameObject>();

    private void OnEnable()
    {
        StartCoroutine(Attack());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _accessibleEnemies.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _accessibleEnemies.Remove(collision.gameObject);
        }
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            if (_accessibleEnemies.Count > 0)
            {
                ShootBullet?.Invoke(gameObject, _accessibleEnemies[0]);
                yield return new WaitForSeconds(_attackCooldown);
            } else
            {
                yield return null;
            }
        }
    }

    public int GetDamage()
    {
        return _damage;
    }

    public void LevelUp()
    {
        TowerLevelUp?.Invoke(gameObject.GetComponent<Tower>());
    }

    public void LevelUpStats()
    {
        _damage += _damageGain;
        if (_attackCooldown > 0.5)
        {
            _attackCooldown -= _cooldownDecrease;
        }
    }

    public void SetStats(int damage, float cooldown)
    {
        _damage = damage;
        _attackCooldown = cooldown;
    }
}
