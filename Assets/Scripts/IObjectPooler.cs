using UnityEngine;

public interface IObjectPooler
{
    GameObject GetPooledObject(string tag);
    void HideAll();
}
