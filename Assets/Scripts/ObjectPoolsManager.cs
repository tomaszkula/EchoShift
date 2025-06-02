using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolsManager : BaseGameManager
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
        GhostIndicatorEntry,
    }

    [Header("Settings")]
    [SerializeField] private List<PoolData> poolsData = new List<PoolData>();

    protected override void InitializeInternal()
    {
        base.InitializeInternal();

        for (int i = 0; i < poolsData.Count; i++)
        {
            PoolData poolData = poolsData[i];
            poolData.pool = new ObjectPool<GameObject>(() => OnCreatePool(poolData), OnGetPooledObject, OnReleasePooledObject);
        }
    }

    protected override void DeinitializeInternal()
    {
        base.DeinitializeInternal();

        for (int i = 0; i < poolsData.Count; i++)
        {
            poolsData[i].pool.Clear();
        }
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
