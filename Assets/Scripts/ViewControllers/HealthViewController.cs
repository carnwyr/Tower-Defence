using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthViewController : MonoBehaviour
{
    [SerializeField]
    private GameObject _healthPrefab;

    private Text _health;

    public void Init(GameObject canvas)
    {
        var health = Instantiate(_healthPrefab);
        health.transform.SetParent(canvas.transform, false);

        _health = health.GetComponent<Text>();
    }

    public void SetCallbacks(IHealthController healthController)
    {
        healthController.HealthChanged += SetHealth;
    }

    private void SetHealth(int health)
    {
        _health.text = health.ToString();
    }
}
