using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; } = null;
    public static bool IsInitialized { get; private set; } = false;

    private BaseManager[] managers = new BaseManager[0];

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        IsInitialized = true;

        DontDestroyOnLoad(gameObject);

        managers = GetComponentsInChildren<BaseManager>();
    }

    private void OnDestroy()
    {
        if (Instance != this)
            return;

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

    public bool AreManagersInitialized()
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

    public T GetManager<T>() where T : BaseManager
    {
        foreach (var manager in managers)
        {
            if (manager is T typedManager)
            {
                return typedManager;
            }
        }

        return null;
    }
}
