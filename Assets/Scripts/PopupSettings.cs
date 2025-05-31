using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupSettings : Popup
{
    [Header("Settings Popup")]
    [SerializeField] private Slider musicVolumeSlider = null;
    [SerializeField] private TextMeshProUGUI musicVolumeValueTMP = null;
    [SerializeField] private Slider soundVolumeSlider = null;
    [SerializeField] private TextMeshProUGUI soundVolumeValueTMP = null;
    [SerializeField] private Button mainMenuButton = null;
    [SerializeField] private Button quitButton = null;

    protected override void OnEnable()
    {
        base.OnEnable();

        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeSliderChanged);
        soundVolumeSlider.onValueChanged.AddListener(OnSoundVolumeSliderChanged);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        musicVolumeSlider.onValueChanged.RemoveListener(OnMusicVolumeSliderChanged);
        soundVolumeSlider.onValueChanged.RemoveListener(OnSoundVolumeSliderChanged);
        mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        quitButton.onClick.RemoveListener(OnQuitButtonClicked);
    }

    public override void Show()
    {
        base.Show();

        musicVolumeSlider.wholeNumbers = true;
        musicVolumeSlider.maxValue = 100;
        musicVolumeSlider.minValue = 0;
        musicVolumeSlider.value = ManagersController.Instance.GetManager<AudioManager>().MusicVolume;

        soundVolumeSlider.wholeNumbers = true;
        soundVolumeSlider.maxValue = 100;
        soundVolumeSlider.minValue = 0;
        soundVolumeSlider.value = ManagersController.Instance.GetManager<AudioManager>().SoundVolume;
    }

    private void OnMusicVolumeSliderChanged(float value)
    {
        Debug.Log($"Music Volume Changed: {value}");

        ManagersController.Instance.GetManager<AudioManager>().MusicVolume = (int)value;
        musicVolumeValueTMP.text = $"{value}%";
    }

    private void OnSoundVolumeSliderChanged(float value)
    {
        Debug.Log($"Sound Volume Changed: {value}");

        ManagersController.Instance.GetManager<AudioManager>().SoundVolume = (int)value;
        soundVolumeValueTMP.text = $"{value}%";
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
