using System.Collections.Generic;
using UnityEngine;

public interface IObjectPooler
{
    GameObject GetPooledObject(string tag);
    List<GameObject> GetSeveral(string tag, int number);
    void HideAll();
}
