using System;
using UnityEngine;
using UnityEngine.UI;

public class GoldViewController : MonoBehaviour
{
    [SerializeField]
    private GameObject _GoldPrefab;

    private Text _gold;
    private Action _unsubscribe;

    private void OnDestroy()
    {
        _unsubscribe();
    }

    public void Init(GameObject canvas)
    {
        var gold = Instantiate(_GoldPrefab);
        gold.transform.SetParent(canvas.transform, false);

        _gold = gold.GetComponent<Text>();
    }

    public void SetCallbacks(IGoldController goldController)
    {
        goldController.GoldChanged += SetGold;

        _unsubscribe = () => RemoveCallbacks(goldController);
    }

    private void RemoveCallbacks(IGoldController goldController)
    {
        if (goldController != null)
        {
            goldController.GoldChanged -= SetGold;
        }
    }

    private void SetGold(int gold)
    {
        _gold.text = gold.ToString();
    }
}
