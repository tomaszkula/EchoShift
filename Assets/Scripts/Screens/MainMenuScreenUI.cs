using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreenUI : BaseScreen
{
    [Header("References")]
    [SerializeField] private Button playButton = null;
    [SerializeField] private Button selectLevelButton = null;
    [SerializeField] private Button quitButton = null;

    private void OnEnable()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        selectLevelButton.onClick.AddListener(OnSelectLevelButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClick);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(OnPlayButtonClicked);
        selectLevelButton.onClick.RemoveListener(OnSelectLevelButtonClicked);
        quitButton.onClick.RemoveListener(OnQuitButtonClick);
    }

    private async void OnPlayButtonClicked()
    {
        Manager.Instance.GetManager<AudioManager>().PlayButtonClickSound();

        await Manager.Instance.GetManager<LevelsManager>().Load();
    }

    private void OnSelectLevelButtonClicked()
    {
        Manager.Instance.GetManager<AudioManager>().PlayButtonClickSound();

        mainMenuUI.SelectScreen(MainMenuUI.ScreenType.LevelSelect);
    }

    private void OnQuitButtonClick()
    {
        Manager.Instance.GetManager<AudioManager>().PlayButtonClickSound();

        Application.Quit();
    }
}
