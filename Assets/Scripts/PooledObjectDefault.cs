using System;
using UnityEngine;
using UnityEngine.Pool;

public class PooledObjectDefault : MonoBehaviour, IPooledObject
{
    public IObjectPool<GameObject> Pool {  get; set; }

    public event Action<GameObject> OnGet = null;
    public event Action<GameObject> OnRelease = null;

    public void Get()
    {
        OnGet?.Invoke(gameObject);
    }

    public void Release()
    {
        OnRelease?.Invoke(gameObject);
    }
}
