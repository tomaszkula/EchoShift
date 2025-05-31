using System;
using UnityEngine;

public class PauseManager : BaseManager
{
    public bool IsPaused { get; private set; } = false;

    public event Action OnPause = null;
    public event Action OnResume = null;

    public override void Initialize()
    {
        Time.timeScale = 1f;

        base.Initialize();
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
        Time.timeScale = 1f;

        OnResume?.Invoke();
    }
}
