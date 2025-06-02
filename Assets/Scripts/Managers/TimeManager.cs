using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class TimeManager : BaseManager
{
    private float serverTimeDelay = 0f;
    public float ServerTime { get; private set; } = 0f;

    public event Action<float> OnServerTimeUpdated = null;

    protected override void InitializeInternal()
    {
        base.InitializeInternal();

        serverTimeDelay = 0f;
        ServerTime = 0f;
    }

    protected override void DeinitializeInternal()
    {
        base.DeinitializeInternal();

        OnServerTimeUpdated = null;

        serverTimeDelay = 0f;
        ServerTime = 0f;
    }

    private void Update()
    {
        if (!IsInitialized)
            return;

        CountTime();
    }

    private void CountTime()
    {
        serverTimeDelay += Time.deltaTime;
        if (serverTimeDelay >= 1f)
        {
            serverTimeDelay--;

            ServerTime++;
            OnServerTimeUpdated?.Invoke(ServerTime);
        }
    }
}