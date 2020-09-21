using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerViewController : MonoBehaviour
{
    public void SetCallbacks(ITowerController towerController)
    {
        towerController.TowersPrepared += PlaceTowers;
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
