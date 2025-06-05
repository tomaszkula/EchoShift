using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupSettings : BasePopup
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
        musicVolumeSlider.value = Manager.Instance.GetManager<AudioManager>().MusicVolume;

        soundVolumeSlider.wholeNumbers = true;
        soundVolumeSlider.maxValue = 100;
        soundVolumeSlider.minValue = 0;
        soundVolumeSlider.value = Manager.Instance.GetManager<AudioManager>().SoundVolume;

        Manager.Instance.GetManager<PauseManager>().Pause();
    }

    public override void Hide()
    {
        base.Hide();

        Manager.Instance.GetManager<PauseManager>().Resume();
    }

    private void OnMusicVolumeSliderChanged(float value)
    {
        Debug.Log($"Music Volume Changed: {value}");

        Manager.Instance.GetManager<AudioManager>().MusicVolume = (int)value;
        musicVolumeValueTMP.text = $"{value}%";
    }

    private void OnSoundVolumeSliderChanged(float value)
    {
        Debug.Log($"Sound Volume Changed: {value}");

        Manager.Instance.GetManager<AudioManager>().SoundVolume = (int)value;
        soundVolumeValueTMP.text = $"{value}%";
    }

    private async void OnMainMenuButtonClicked()
    {
        Debug.Log("Returning to Main Menu");

        Manager.Instance.GetManager<AudioManager>().PlayButtonClickSound();

        Hide();

        await Manager.Instance.GetManager<ScenesManager>().LoadMainMenuAsync();
    }

    private void OnQuitButtonClicked()
    {
        Debug.Log("Quitting Game");

        Manager.Instance.GetManager<AudioManager>().PlayButtonClickSound();

        Hide();

        Application.Quit();
    }
}
