using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreenUI : BaseScreen
{
    [Header("References")]
    [SerializeField] private Button playButton = null;
    [SerializeField] private Button selectLevelButton = null;

    private void OnEnable()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        selectLevelButton.onClick.AddListener(OnSelectLevelButtonClicked);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(OnPlayButtonClicked);
        selectLevelButton.onClick.RemoveListener(OnSelectLevelButtonClicked);
    }

    private async void OnPlayButtonClicked()
    {
        Debug.Log("Play button clicked! Starting game...");

        Manager.Instance.GetManager<AudioManager>().PlayButtonClickSound();

        await Manager.Instance.GetManager<LevelsManager>().Load();
    }

    private void OnSelectLevelButtonClicked()
    {
        Debug.Log("Select Level button clicked! Opening level selection...");

        Manager.Instance.GetManager<AudioManager>().PlayButtonClickSound();

        mainMenuUI.SelectScreen(MainMenuUI.ScreenType.LevelSelect);
    }
}
