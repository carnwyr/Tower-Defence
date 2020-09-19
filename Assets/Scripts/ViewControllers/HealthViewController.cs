using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthViewController : MonoBehaviour
{
    [SerializeField]
    private GameObject _healthPrefab;

    private Text _health;
    private Action _unsubscribe;

    private void OnDestroy()
    {
        _unsubscribe();
    }

    public void Init(GameObject canvas)
    {
        var health = Instantiate(_healthPrefab);
        health.transform.SetParent(canvas.transform, false);

        _health = health.GetComponent<Text>();
    }

    public void SetCallbacks(IHealthController healthController)
    {
        healthController.HealthChanged += SetHealth;

        _unsubscribe = () => RemoveCallbacks(healthController);
    }

    private void RemoveCallbacks(IHealthController healthController)
    {
        if (healthController != null)
        {
            healthController.HealthChanged -= SetHealth;
        }
    }

    private void SetHealth(int health)
    {
        _health.text = health.ToString();
    }
}
