using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool IsInitialized { get; private set; } = false;
    public static GameManager Instance { get; private set; } = null;
    public static Action OnInitialized = null;

    private float gameTimeDelay = 0f;
    public float GameTime { get; private set; }
    public event Action<float> OnGameTimeUpdated = null;

    public bool IsPlaying { get; private set; } = false;

    private BaseManager[] managers = new BaseManager[0];

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        managers = GetComponentsInChildren<BaseManager>();

        IsInitialized = true;
        OnInitialized?.Invoke();
    }

    private IEnumerator Start()
    {
        gameTimeDelay = 0f;
        GameTime = 0f;

        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Initialize();
        }

        bool allManagersReady = true;
        do
        {
            allManagersReady = AreAllManagersInitialized();

            if (!allManagersReady)
            {
                yield return new WaitForSeconds(0.1f);
            }
        } while (!allManagersReady);

        IsPlaying = true;
    }

    private void Update()
    {
        CountGameTime();
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
            IsInitialized = false;

            for (int i = 0; i < managers.Length; i++)
            {
                managers[i].Deinitialize();
            }
        }
    }

    public bool AreAllManagersInitialized()
    {
        foreach (var manager in managers)
        {
            if (!manager.IsInitialized)
            {
                return false;
            }
        }
        return true;
    }

    private void CountGameTime()
    {
        if (!IsPlaying)
        {
            return;
        }

        float deltaTime = Time.deltaTime;
        gameTimeDelay += deltaTime;
        GameTime += deltaTime;

        if (gameTimeDelay >= 1f)
        {
            gameTimeDelay--;
            OnGameTimeUpdated?.Invoke(GameTime);
        }
    }

    public T GetManager<T>() where T : BaseManager
    {
        foreach (var manager in managers)
        {
            if (manager is T)
            {
                return manager as T;
            }
        }
        return null;
    }
}