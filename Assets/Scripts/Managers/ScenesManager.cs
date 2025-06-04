using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : BaseManager
{
    [Header("Settings")]
    [SerializeField] private SceneAsset launcherSceneAsset = null;
    [SerializeField] private SceneAsset mainMenuSceneAsset = null;

    public async UniTask LoadMainMenuScene()
    {
        await SceneManager.LoadSceneAsync(mainMenuSceneAsset.name, LoadSceneMode.Single);
    }

    public async UniTask LoadMainScene(string sceneName)
    {
        await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }

    public async UniTask LoadAdditionalScene(string sceneName)
    {
        await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }
}
