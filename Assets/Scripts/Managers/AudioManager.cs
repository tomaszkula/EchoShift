using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : BaseManager
{
    [Header("Settings")]
    [SerializeField] private int defaultVolume = 50;
    [SerializeField] private int defaultMusicVolume = 50;
    [SerializeField] private int defaultSoundVolume = 50;
    [SerializeField] private AudioClip musicAC = null;
    [SerializeField] private AudioClip buttonClickAC = null;

    [Header("References")]
    [SerializeField] private AudioMixer audioMixer = null;
    [SerializeField] private AudioSource musicAS = null;
    [SerializeField] private AudioSource soundAS = null;

    private const string AUDIO_MIXER_VOLUME_KEY = "Volume";
    private const string AUDIO_MIXER_MUSIC_VOLUME_KEY = "MusicVolume";
    private const string AUDIO_MIXER_SOUND_VOLUME_KEY = "SoundVolume";

    private const string VOLUME_KEY = "Volume";
    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    private const string SOUND_VOLUME_KEY = "SoundVolume";

    public int Volume
    {
        get => Manager.Instance.GetManager<SaveManager>().GetValue(VOLUME_KEY, defaultVolume);
        set
        {
            Manager.Instance.GetManager<SaveManager>().SetValue(VOLUME_KEY, value);
            audioMixer.SetFloat(AUDIO_MIXER_VOLUME_KEY, LinearToDecibel(value, 0, 100));
        }
    }

    public int MusicVolume
    {
        get => Manager.Instance.GetManager<SaveManager>().GetValue(MUSIC_VOLUME_KEY, defaultMusicVolume);
        set 
        {
            Manager.Instance.GetManager<SaveManager>().SetValue(MUSIC_VOLUME_KEY, value);
            audioMixer.SetFloat(AUDIO_MIXER_MUSIC_VOLUME_KEY, LinearToDecibel(value, 0, 100));
        }
    }

    public int SoundVolume
    {
        get => Manager.Instance.GetManager<SaveManager>().GetValue(SOUND_VOLUME_KEY, defaultSoundVolume);
        set 
        {
            Manager.Instance.GetManager<SaveManager>().SetValue(SOUND_VOLUME_KEY, value);
            audioMixer.SetFloat(AUDIO_MIXER_SOUND_VOLUME_KEY, LinearToDecibel(value, 0, 100));
        }
    }

    protected override async void InitializeInternal()
    {
        base.InitializeInternal();

        await new WaitUntil(() => Manager.Instance.GetManager<SaveManager>().IsInitialized);

        audioMixer.SetFloat(AUDIO_MIXER_VOLUME_KEY, LinearToDecibel(Volume, 0, 100));
        audioMixer.SetFloat(AUDIO_MIXER_MUSIC_VOLUME_KEY, LinearToDecibel(MusicVolume, 0, 100));
        audioMixer.SetFloat(AUDIO_MIXER_SOUND_VOLUME_KEY, LinearToDecibel(SoundVolume, 0, 100));

        PlayMusic();
    }

    protected override void DeinitializeInternal()
    {
        base.DeinitializeInternal();

        StopMusic();
    }

    public void PlayMusic()
    {
        musicAS.clip = musicAC;
        musicAS.Play();
    }

    public void StopMusic()
    {
        musicAS.Stop();
    }

    public void PlayButtonClickSound()
    {
        soundAS.PlayOneShot(buttonClickAC);
    }

    public static float LinearToDecibel(float value, float min, float max)
    {
        value = Mathf.Clamp(value, min, max);
        value = Mathf.Lerp(0, 1, (value - min) / (max - min));

        if (value <= 0.0001f)
            return -80f;

        return 20f * Mathf.Log10(value);
    }
}
