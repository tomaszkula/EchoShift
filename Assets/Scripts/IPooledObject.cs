using System;
using UnityEngine;
using UnityEngine.Pool;

public interface IPooledObject
{
    IObjectPool<GameObject> Pool { get; set; }
    void Get();
    void Release();
    event Action<GameObject> OnGet;
    event Action<GameObject> OnRelease;
}
