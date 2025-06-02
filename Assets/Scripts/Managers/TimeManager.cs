using System;
using UnityEngine;

public class TimeManager : BaseGameManager
{
    private float gameTimeDelay = 0f;
    public float GameTime { get; private set; } = 0f;

    public event Action<float> OnGameTimeUpdated = null;

    protected override void InitializeInternal()
    {
        base.InitializeInternal();

        gameTimeDelay = 0f;
        GameTime = 0f;
    }

    protected override void DeinitializeInternal()
    {
        base.DeinitializeInternal();

        OnGameTimeUpdated = null;

        gameTimeDelay = 0f;
        GameTime = 0f;
    }

    private void Update()
    {
        CountTime();
    }

    private void CountTime()
    {
        if (!IsInitialized)
            return;

        float deltaTime = Time.deltaTime;
        gameTimeDelay += deltaTime;
        GameTime += deltaTime;

        if (gameTimeDelay >= 1f)
        {
            gameTimeDelay--;

            OnGameTimeUpdated?.Invoke(GameTime);
        }
    }
}