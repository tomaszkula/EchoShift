using UnityEngine.SceneManagement;

public class ScenesManager : BaseManager
{
    public const string LAUNCHER_SCENE_NAME = "LauncherScene";
    public const string MAIN_MENU_SCENE_NAME = "MainMenuScene";
    public const string GAME_SCENE_NAME = "GameScene";

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
