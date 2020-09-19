using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private int _damage = 5;
    private float _attackCooldown = 2f;

    private List<GameObject> _accessibleEnemies = new List<GameObject>();

    private void Awake()
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
                _accessibleEnemies[0].GetComponent<Enemy>().GetAttacked(_damage);
                yield return new WaitForSeconds(_attackCooldown);
            } else
            {
                yield return null;
            }
        }
    }
}
