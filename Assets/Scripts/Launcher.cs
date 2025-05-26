using System.Collections;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    private ManagersController _managersController = null;

    private void Awake()
    {
        _managersController = FindAnyObjectByType<ManagersController>();
    }

    private IEnumerator Start()
    {
        if (_managersController == null)
        {
            Debug.LogError("ManagersController not found in the scene. Please ensure it is present.");
            yield break;
        }

        _managersController.InitializeManagers();
        while (!_managersController.AreAllManagersInitialized())
        {
            yield return null;
        }

        Debug.Log("ManagersController initialized successfully. Starting game...");

        ManagersController.Instance.GetManager<ScenesManager>().LoadScene(ScenesManager.MAIN_MENU_SCENE_NAME);
    }
}
