using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupSettings : BasePopup
{
    [Header("References")]
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private TextMeshProUGUI volumeValueTMP = null;
    [SerializeField] private Slider musicVolumeSlider = null;
    [SerializeField] private TextMeshProUGUI musicVolumeValueTMP = null;
    [SerializeField] private Slider soundVolumeSlider = null;
    [SerializeField] private TextMeshProUGUI soundVolumeValueTMP = null;

    protected override void OnEnable()
    {
        base.OnEnable();

        volumeSlider.onValueChanged.AddListener(OnVolumeSliderChanged);
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeSliderChanged);
        soundVolumeSlider.onValueChanged.AddListener(OnSoundVolumeSliderChanged);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        volumeSlider.onValueChanged.RemoveListener(OnVolumeSliderChanged);
        musicVolumeSlider.onValueChanged.RemoveListener(OnMusicVolumeSliderChanged);
        soundVolumeSlider.onValueChanged.RemoveListener(OnSoundVolumeSliderChanged);
    }

    public override void Show()
    {
        base.Show();

        volumeSlider.wholeNumbers = true;
        volumeSlider.maxValue = 100;
        volumeSlider.minValue = 0;
        volumeSlider.value = Manager.Instance.GetManager<AudioManager>().Volume;

        musicVolumeSlider.wholeNumbers = true;
        musicVolumeSlider.maxValue = 100;
        musicVolumeSlider.minValue = 0;
        musicVolumeSlider.value = Manager.Instance.GetManager<AudioManager>().MusicVolume;

        soundVolumeSlider.wholeNumbers = true;
        soundVolumeSlider.maxValue = 100;
        soundVolumeSlider.minValue = 0;
        soundVolumeSlider.value = Manager.Instance.GetManager<AudioManager>().SoundVolume;
    }

    private void OnVolumeSliderChanged(float value)
    {
        Debug.Log($"Volume Changed: {value}");

        Manager.Instance.GetManager<AudioManager>().Volume = (int)value;
        volumeValueTMP.text = $"{value}%";
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
}
