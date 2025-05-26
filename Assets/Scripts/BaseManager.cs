using System;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    public bool isInitialized { get; private set; } = false;
    public event Action onInitialized;
    public event Action onDeinitialized;

    public virtual void Initialize()
    {
        Debug.Log($"{GetType()} initialized.");

        isInitialized = true;
        onInitialized?.Invoke();
    }

    public virtual void Deinitialize()
    {
        Debug.Log($"{GetType()} deinitialized.");

        isInitialized = false;
        onDeinitialized?.Invoke();
    }
}
