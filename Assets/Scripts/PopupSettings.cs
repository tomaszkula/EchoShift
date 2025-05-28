using UnityEngine;
using UnityEngine.UI;

public class PopupSettings : Popup
{
    [Header("Settings Popup")]
    [SerializeField] private Button mainMenuButton = null;
    [SerializeField] private Button quitButton = null;

    protected override void OnEnable()
    {
        base.OnEnable();

        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        quitButton.onClick.RemoveListener(OnQuitButtonClicked);
    }

    private void OnMainMenuButtonClicked()
    {
        Debug.Log("Returning to Main Menu");

        ManagersController.Instance.GetManager<AudioManager>().PlayButtonClickSound();

        ManagersController.Instance.GetManager<ScenesManager>().LoadScene(ScenesManager.MAIN_MENU_SCENE_NAME);
    }

    private void OnQuitButtonClicked()
    {
        Debug.Log("Quitting Game");

        ManagersController.Instance.GetManager<AudioManager>().PlayButtonClickSound();

        Application.Quit();
    }
}
