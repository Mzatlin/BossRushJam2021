using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public int amountToPool;
    public GameObject objectToPool;
}


public class ObjectPooler : MonoBehaviour
{
    [SerializeField] List<ObjectPoolItem> objectsToPool = new List<ObjectPoolItem>();

   [SerializeField] List<GameObject> itemPoolObjects = new List<GameObject>();

    public static ObjectPooler Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        foreach(ObjectPoolItem item in objectsToPool)
        {
            IncreasePoolSize(item.objectToPool, item.amountToPool);
        }

    }

    void IncreasePoolSize(GameObject objectToPool, int amountToPool)
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject addedObject = Instantiate(objectToPool);
            addedObject.transform.SetParent(transform);
            addedObject.SetActive(false);
            itemPoolObjects.Add(addedObject);
        }
    }

    public GameObject GetFromPool(string tag)
    {
        for (int i = 0; i < itemPoolObjects.Count; i++)
        {
            if (itemPoolObjects[i].CompareTag(tag) && !itemPoolObjects[i].activeInHierarchy)
            {
                itemPoolObjects[i].SetActive(true);
                return itemPoolObjects[i];
            }
        }
        foreach (ObjectPoolItem item in objectsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                IncreasePoolSize(item.objectToPool, item.amountToPool);
                return GetFromPool(tag);
            }
        }
        return null;
    }

    public void ClearPool(string tag)
    {
        for (int i = 0; i < itemPoolObjects.Count; i++)
        {
            if (itemPoolObjects[i].CompareTag(tag) && itemPoolObjects[i].activeInHierarchy)
            {
                itemPoolObjects[i].SetActive(false);
            }
        }
    }
}
