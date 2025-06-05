using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : BaseManager
{
    private const string LAUNCHER_SCENE_NAME = "LauncherScene";
    private const string MAIN_MENU_SCENE_NAME = "MainMenuScene";

    public async UniTask LoadLauncherAsync()
    {
        await LoadAsync(LAUNCHER_SCENE_NAME, LoadSceneMode.Single);
    }

    public async UniTask LoadMainMenuAsync()
    {
        await LoadAsync(MAIN_MENU_SCENE_NAME, LoadSceneMode.Single);
    }

    public async UniTask LoadAsync(string sceneName, LoadSceneMode mode)
    {
        await SceneManager.LoadSceneAsync(sceneName, mode);
    }
}
