using System;
using UnityEngine;
using UnityEngine.Pool;

public class PooledObjectDefault : MonoBehaviour, IPooledObject
{
    public ObjectsPool ObjectsPool { get; set; }
    public IObjectPool<GameObject> Pool {  get; set; }

    public event Action<GameObject> OnGet = null;
    public event Action<GameObject> OnRelease = null;

    public bool IsInPool { get; private set; } = false;

    private void OnDestroy()
    {
        ObjectsPool.OnUnregistered -= OnPoolUnregistered;
    }

    public void Create()
    {
        ObjectsPool.OnUnregistered += OnPoolUnregistered;

        IsInPool = true;
    }

    public void Get()
    {
        IsInPool = false;

        OnGet?.Invoke(gameObject);
    }

    public void Release()
    {
        IsInPool = true;

        OnRelease?.Invoke(gameObject);
    }

    private void OnPoolUnregistered()
    {
        if (gameObject != null && !IsInPool)
        {
            Destroy(gameObject);
        }
    }
}
