using System.Collections;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    private Manager manager = null;

    private void Awake()
    {
        manager = FindAnyObjectByType<Manager>();
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => manager.AreAllManagersInitialized());

        Debug.Log("ManagersController initialized successfully. Starting game...");

        Manager.Instance.GetManager<ScenesManager>().LoadScene(ScenesManager.MAIN_MENU_SCENE_NAME);
    }
}
