using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolsManager : BaseManager
{
    [Serializable]
    public class PoolData
    {
        public PoolType poolType;
        public GameObject prefab = null;

        [NonSerialized] public IObjectPool<GameObject> pool = null;
    }

    public enum PoolType
    {
        Projectile,
        Ghost,
        Player,
    }

    [Header("Settings")]
    [SerializeField] private List<PoolData> poolsData = new List<PoolData>();

    public override void Initialize()
    {
        for (int i = 0; i < poolsData.Count; i++)
        {
            PoolData poolData = poolsData[i];
            poolData.pool = new ObjectPool<GameObject>(() => OnCreatePool(poolData), OnGetPooledObject, OnReleasePooledObject);
            //poolData.pool.Release(poolsData[i].pool.Get());
        }

        base.Initialize();
    }

    public override void Deinitialize()
    {
        for (int i = 0; i < poolsData.Count; i++)
        {
            poolsData[i].pool.Clear();
        }

        base.Deinitialize();
    }

    public IObjectPool<GameObject> GetPool(PoolType type)
    {
        for (int i = 0; i < poolsData.Count; i++)
        {
            if (poolsData[i].poolType == type)
            {
                return poolsData[i].pool;
            }
        }

        return null;
    }

    private GameObject OnCreatePool(PoolData poolData)
    {
        GameObject go = Instantiate(poolData.prefab, transform);
        if(go.TryGetComponent(out IPooledObject pooledObject))
        {
            pooledObject.Pool = poolData.pool;
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
