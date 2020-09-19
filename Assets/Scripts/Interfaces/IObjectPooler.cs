using System;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectPooler
{
    event Action<GameObject> NewObjectCreated;
    GameObject GetPooledObject(string tag);
    List<GameObject> GetSeveral(string tag, int number);
    List<GameObject> GetFullList(string tag);
    void HideAll();
}
