using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;
    public static bool IsInitialized { get; private set; } = false;

    public bool IsPlaying { get; private set; } = false;

    private BaseGameManager[] managers = new BaseGameManager[0];

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        IsInitialized = true;

        managers = GetComponentsInChildren<BaseGameManager>();
    }

    private IEnumerator Start()
    {
        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Initialize();
        }

        yield return new WaitUntil(() => AreAllManagersInitialized());

        IsPlaying = true;

        yield return new WaitUntil(() => Manager.Instance.GetManager<LevelsManager>().IsLevelLoaded);
        GetManager<PlayerManager>().Spawn();
    }

    private void OnDestroy()
    {
        if (Instance != this)
            return;

        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Deinitialize();
        }

        Instance = null;
        IsInitialized = false;
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

    public T GetManager<T>() where T : BaseGameManager
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