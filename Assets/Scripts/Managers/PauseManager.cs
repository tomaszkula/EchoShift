using System;
using UnityEngine;

public class PauseManager : BaseManager
{
    [Header("Settings")]
    [SerializeField] private float defaultTimeScale = 1f;

    public bool IsPaused { get; private set; } = false;

    public event Action OnPaused = null;
    public event Action OnResumed = null;

    protected override void InitializeInternal()
    {
        base.InitializeInternal();

        IsPaused = false;
        Time.timeScale = defaultTimeScale;
    }

    protected override void DeinitializeInternal()
    {
        base.DeinitializeInternal();

        OnPaused = null;
        OnResumed = null;
    }

    public void Pause()
    {
        Debug.Log("Game Paused");

        IsPaused = true;
        Time.timeScale = 0f;

        OnPaused?.Invoke();
    }

    public void Resume()
    {
        Debug.Log("Game Resumed");

        IsPaused = false;
        Time.timeScale = defaultTimeScale;

        OnResumed?.Invoke();
    }
}
