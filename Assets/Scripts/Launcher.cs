using Cysharp.Threading.Tasks;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    private Manager manager = null;

    private void Awake()
    {
        manager = FindAnyObjectByType<Manager>();
    }

    private async void Start()
    {
        manager.InitializeManagers();
        await new WaitUntil(() => manager.AreManagersInitialized());

        Debug.Log("Manager initialized successfully. Starting game...");

        await Manager.Instance.GetManager<ScenesManager>().LoadMainMenuAsync();
    }
}
