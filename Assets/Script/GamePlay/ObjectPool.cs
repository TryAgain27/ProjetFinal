using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PoolInformation
{
    public GameObject objectToPool;
    public int poolCount;
    public List<GameObject> pooledObjects = new();
}


public interface IPoolable
{
    void Reset();
}


public class ObjectPool : MonoBehaviour
{

    [SerializeField] List<PoolInformation> objectToBePooled;

    private static ObjectPool instance;

    public static ObjectPool GetInstance() => instance;
    private int poolIndex;


    //--------------------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        instance = this;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    private void Start()
    {
        foreach (var pool in objectToBePooled)
        {

            for (int i = 0; i < pool.poolCount; i++)
            {
                GameObject gameObject = Instantiate(pool.objectToPool, Vector3.zero, Quaternion.identity, transform);
                gameObject.SetActive(false);
                pool.pooledObjects.Add(gameObject);
            }
        }

    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

    public GameObject GetObject(GameObject obj)
    {
        foreach (var pool in objectToBePooled)
        {
            if (pool.objectToPool.ToString() == obj.ToString())
            {

                poolIndex %= pool.poolCount;
                GameObject poolObject = pool.pooledObjects[poolIndex++];
                poolObject.GetComponent<IPoolable>().Reset();
                return poolObject;
            }
        }
        return null;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------

}
