using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectsPoolsManager : BaseManager
{
    [Serializable]
    public class Data
    {
        public ObjectsPoolType type = null;
        public GameObject prefab = null;

        [NonSerialized] public ObjectPool<GameObject> pool = null;
    }

    [Header("References")]
    [SerializeField] private ObjectsPoolType ghostOPT = null;
    [SerializeField] private ObjectsPoolType playerOPT = null;
    [SerializeField] private ObjectsPoolType uiGhostIndicatorEntryOPT = null;
    [SerializeField] private ObjectsPoolType mainMenuLevelStarEntryOPT = null;

    private List<ObjectsPool> objectsPools = new List<ObjectsPool>();
    private Dictionary<ObjectsPoolType, Data> pools = new Dictionary<ObjectsPoolType, Data>();

    public ObjectsPoolType GhostOPT => ghostOPT;
    public ObjectsPoolType PlayerOPT => playerOPT;
    public ObjectsPoolType UiGhostIndicatorEntryOPT => uiGhostIndicatorEntryOPT;
    public ObjectsPoolType MainMenuLevelStarEntryOPT => mainMenuLevelStarEntryOPT;

    public void Register(ObjectsPool objectsPool)
    {
        List<Data> poolsData = objectsPool.ObjectsPoolsData;
        for (int i = 0; i < poolsData.Count; i++)
        {
            Data poolData = poolsData[i];
            poolData.pool = new ObjectPool<GameObject>(() => OnCreatePool(objectsPool, poolData), OnGetPooledObject, OnReleasePooledObject);
            for (int j = 0; j < poolData.type.SpawnCount; j++)
                poolData.pool.Release(poolData.pool.Get());
            pools.Add(poolData.type, poolData);
        }

        objectsPools.Add(objectsPool);
    }

    public void Unregister(ObjectsPool objectsPoolData)
    {
        List<Data> poolsData = objectsPoolData.ObjectsPoolsData;
        for (int i = 0; i < poolsData.Count; i++)
        {
            Data poolData = poolsData[i];
            poolData.pool.Clear();
            pools.Remove(poolData.type);
        }

        objectsPools.Remove(objectsPoolData);
    }

    public ObjectPool<GameObject> GetPool(ObjectsPoolType type)
    {
        return pools[type].pool;
    }

    private GameObject OnCreatePool(ObjectsPool op, Data poolData)
    {
        GameObject go = Instantiate(poolData.prefab, op.transform);
        if(go.TryGetComponent(out IPooledObject pooledObject))
        {
            pooledObject.ObjectsPool = op;
            pooledObject.Pool = poolData.pool;

            pooledObject.Create();
        }

        return go;
    }

    private void OnGetPooledObject(GameObject go)
    {
        go.gameObject.SetActive(true);

        if (go.TryGetComponent(out IPooledObject pooledObject))
        {
            pooledObject.Get();
        }
    }

    private void OnReleasePooledObject(GameObject go)
    {
        if (go.TryGetComponent(out IPooledObject pooledObject))
        {
            pooledObject.Release();
        }

        go.gameObject.SetActive(false);
    }
}
