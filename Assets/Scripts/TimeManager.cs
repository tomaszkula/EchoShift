using System;
using UnityEngine;

public class TimeManager : BaseManager
{
    private float _serverTimeDelay = 0f;

    private float _serverTime = 0f;
    public float ServerTime
    {
        get => _serverTime;
        set
        {
            _serverTime = value;
            OnServerTimeUpdated?.Invoke(_serverTime);
        }
    }
    public event Action<float> OnServerTimeUpdated = null;

    public override void Initialize()
    {
        _serverTimeDelay = 0f;
        _serverTime = 0f;

        base.Initialize();
    }

    private void Update()
    {
        if(!isInitialized)
            return;

        CountTime();
    }

    private void CountTime()
    {
        _serverTimeDelay += Time.deltaTime;
        if (_serverTimeDelay >= 1f)
        {
            _serverTimeDelay--;
            ServerTime++;
        }
    }
}