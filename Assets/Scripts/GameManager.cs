using Cysharp.Threading.Tasks;
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

    private async void Start()
    {
        Manager.Instance.GetManager<TimeManager>().ResetTimer();

        InitializeManagers();
        await new WaitUntil(() => AreAllManagersInitialized());

        IsPlaying = true;

        await new WaitUntil(() => Manager.Instance.GetManager<LevelsManager>().IsLevelLoaded);

        Manager.Instance.GetManager<TimeManager>().IsCounting = true;
        Manager.Instance.GetManager<PlayerManager>().Spawn();
    }

    private void OnDestroy()
    {
        if (Instance != this)
            return;

        DeinitializeManagers();

        Instance = null;
        IsInitialized = false;
    }

    public void InitializeManagers()
    {
        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Initialize();
        }
    }

    public void DeinitializeManagers()
    {
        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Deinitialize();
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