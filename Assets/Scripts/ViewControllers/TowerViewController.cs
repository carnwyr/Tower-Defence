using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerViewController : MonoBehaviour
{
    private Action _unsubscribe;

    private void OnDestroy()
    {
        _unsubscribe();
    }

    public void SetCallbacks(ITowerController towerController)
    {
        towerController.TowersPrepared += PlaceTowers;

        _unsubscribe = () => RemoveCallbacks(towerController);
    }

    private void RemoveCallbacks(ITowerController towerController)
    {
        if (towerController != null)
        {
            towerController.TowersPrepared -= PlaceTowers;
        }
    }

    private void PlaceTowers(List<GameObject> towers, List<Vector2> positions)
    {
        for (int i = 0; i < towers.Count; i++)
        {
            towers[i].transform.position = positions[i];
            towers[i].SetActive(true);
        }
    }
}
