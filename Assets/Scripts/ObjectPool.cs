using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int poolSize = 10;
    private List<GameObject> objectPoolList = new List<GameObject>();





    void Start()
    {
        CreatePool();
    }

    /// <summary>
    /// Initialize the object pool by creating a specified number of objects and adding them to the pool list
    /// </summary>
    private void CreatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObject();
        }
    }
    /// <summary>
    /// Instantiate new Object and add it to the pool list
    /// </summary>
    /// <returns>GameObject</returns>
    private GameObject CreateNewObject()
    {
        GameObject newObject = Instantiate(objectPrefab);
        newObject.SetActive(false);
        objectPoolList.Add(newObject);
        return newObject;
    }



    /// <summary>
    /// Get an object from the pool. If no inactive object is available, create a new one.
    /// </summary>
    /// <returns>GameObject</returns>
    public GameObject GetObjectFromPool()
    {
        GameObject theObject = objectPoolList.Find(obj => !obj.activeInHierarchy);
        if (theObject == null)
        {
            theObject = CreateNewObject();
        }
        theObject.SetActive(true);
        return theObject;
    }
}
