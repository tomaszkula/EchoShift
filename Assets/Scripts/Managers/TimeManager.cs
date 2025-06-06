using System;
using UnityEngine;

public class TimeManager : BaseManager
{
    public bool IsCounting { get; set; } = false;
    public float GameTimeDelay { get; private set; } = 0f;
    public float GameTime { get; private set; } = 0f;

    public event Action<float> OnGameTimeUpdated = null;

    private void Update()
    {
        CountTime();
    }

    protected override void InitializeInternal()
    {
        base.InitializeInternal();

        ResetManager();
    }

    protected override void DeinitializeInternal()
    {
        base.DeinitializeInternal();

        OnGameTimeUpdated = null;
    }

    private void CountTime()
    {
        if (!IsInitialized || !IsCounting ||
            Manager.Instance.GetManager<PauseManager>().IsPaused)
            return;

        float deltaTime = Time.deltaTime;
        GameTimeDelay += deltaTime;
        GameTime += deltaTime;

        if (GameTimeDelay >= 1f)
        {
            GameTimeDelay--;

            OnGameTimeUpdated?.Invoke(GameTime);
        }
    }

    public void ResetManager()
    {
        IsCounting = false;
        GameTimeDelay = 0f;
        GameTime = 0f;
    }
}