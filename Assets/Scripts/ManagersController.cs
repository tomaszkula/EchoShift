using System.Collections;
using UnityEngine;

public class ManagersController : MonoBehaviour
{
    public static ManagersController Instance { get; private set; }

    private BaseManager[] managers = new BaseManager[0];

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        managers = GetComponentsInChildren<BaseManager>();
    }

    public void InitializeManagers()
    {
        StartCoroutine(InitializeManagersCoroutine());
    }

    private IEnumerator InitializeManagersCoroutine()
    {
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
    }

    public bool AreAllManagersInitialized()
    {
        foreach (var manager in managers)
        {
            if (!manager.isInitialized)
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
