using Cysharp.Threading.Tasks;
using System.Collections;
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
        await new WaitUntil(() => manager.AreAllManagersInitialized());

        Debug.Log("ManagersController initialized successfully. Starting game...");

        await Manager.Instance.GetManager<ScenesManager>().LoadMainMenuScene();
    }
}
