using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pools
{
    public int _PoolSize = 0;
    public GameObject _PooledObject = null;
    public bool _ShouldGrow = false;
}

public class ObjectPoolManager : MonoBehaviour
{
    #region Lists
    [SerializeField] private List<Pools> _ObjectPools = null;
    public List<GameObject> _PooledObjects = null;
    #endregion

    #region Singleton
    public static ObjectPoolManager PoolerInstance = null;

    private void Awake()
    {
        PoolerInstance = this;
    }
    #endregion

    void Start()
    {
        InstantiateAndFillPool();
    }

    private void InstantiateAndFillPool()
    {
        _PooledObjects = new List<GameObject>();
        foreach (Pools pool in _ObjectPools)
        {
            for (int i = 0; i < pool._PoolSize; i++)
            {
                GameObject obj = (GameObject)Instantiate(pool._PooledObject);
                obj.SetActive(false);
                _PooledObjects.Add(obj);
            }
        }
    }

    public GameObject Get(string Needed_Obj_Tag)
    {
        for (int i = 0 ; i < _PooledObjects.Count ; i++)
        {
            GameObject obj = _PooledObjects[i];
            if (!obj.activeInHierarchy && obj.tag == Needed_Obj_Tag)
            {
                return obj;
            }
        }

        foreach(Pools pool in _ObjectPools)
        {
            if(pool._PooledObject.tag == Needed_Obj_Tag)
            {
                if(pool._ShouldGrow)
                {
                    GameObject obj = (GameObject)Instantiate(pool._PooledObject);
                    _PooledObjects.Add(obj);
                    obj.SetActive(false);
                    return obj;
                }
            }
        }

        Debug.LogError("All Pooled Objects Are Active In Hierarchy");
        return null;
    }

}
