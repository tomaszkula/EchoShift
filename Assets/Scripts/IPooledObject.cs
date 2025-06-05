using System;
using UnityEngine;
using UnityEngine.Pool;

public interface IPooledObject
{
    ObjectsPool ObjectsPool { get; set; }
    IObjectPool<GameObject> Pool { get; set; }
    void Create();
    void Get();
    void Release();
    event Action<GameObject> OnGet;
    event Action<GameObject> OnRelease;
}
