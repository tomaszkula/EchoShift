using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class PauseManager : BaseManager
{
    [Header("Settings")]
    [SerializeField] private float defaultTimeScale = 1f;

    public bool IsPaused { get; private set; } = false;

    public event Action OnPause = null;
    public event Action OnResume = null;

    protected override void InitializeInternal()
    {
        base.InitializeInternal();

        IsPaused = false;
        Time.timeScale = defaultTimeScale;
    }

    protected override void DeinitializeInternal()
    {
        base.DeinitializeInternal();

        OnPause = null;
        OnResume = null;

        IsPaused = false;
        Time.timeScale = defaultTimeScale;
    }

    public void Pause()
    {
        Debug.Log("Game Paused");

        IsPaused = true;
        Time.timeScale = 0f;

        OnPause?.Invoke();
    }

    public void Resume()
    {
        Debug.Log("Game Resumed");

        IsPaused = false;
        Time.timeScale = defaultTimeScale;

        OnResume?.Invoke();
    }
}
