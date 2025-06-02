using System;
using UnityEngine;

public class BaseGameManager : MonoBehaviour
{
    public bool IsInitialized { get; private set; } = false;

    public event Action onInitialized = null;
    public event Action onDeinitialized = null;

    public void Initialize()
    {
        if (IsInitialized)
            return;

        InitializeInternal();

        Debug.Log($"{GetType()} initialized.");

        IsInitialized = true;
        onInitialized?.Invoke();
    }

    public void Deinitialize()
    {
        if (!IsInitialized)
            return;

        DeinitializeInternal();

        Debug.Log($"{GetType()} deinitialized.");

        IsInitialized = false;
        onDeinitialized?.Invoke();
    }

    protected virtual void InitializeInternal()
    {

    }

    protected virtual void DeinitializeInternal()
    {

    }
}
