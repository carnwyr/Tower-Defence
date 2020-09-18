using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public GameObject ObjectToPool;
    public int AmountToPool;
}

public class ObjectPooler : MonoBehaviour, IObjectPooler
{
    public List<ObjectPoolItem> itemsToPool;

    private List<GameObject> pooledObjects;

    public void Awake()
    {
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.AmountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.ObjectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].CompareTag(tag))
            {
                return pooledObjects[i];
            }
        }

        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.ObjectToPool.CompareTag(tag))
            {
                GameObject obj = (GameObject)Instantiate(item.ObjectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
                return obj;
            }
        }
        return null;
    }
    public List<GameObject> GetSeveral(string tag, int number)
    {
        List<GameObject> objects = new List<GameObject>();

        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].CompareTag(tag))
            {
                objects.Add(pooledObjects[i]);
                if (objects.Count == number)
                {
                    return objects;
                }
            }
        }

        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.ObjectToPool.CompareTag(tag))
            {
                for (int i = 0; i < number - objects.Count; i++)
                {
                    GameObject obj = (GameObject)Instantiate(item.ObjectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    objects.Add(obj);
                }
            }
        }
        return objects;
    }

    public void HideAll()
    {
        foreach (var obj in pooledObjects)
        {
            if (obj.activeInHierarchy)
            {
                obj.SetActive(false);
            }
        }
    }
}
