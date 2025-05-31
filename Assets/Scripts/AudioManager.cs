using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.Rendering.DebugUI;

public class AudioManager : BaseManager
{
    [Header("Settings")]
    [SerializeField] private AudioClip musicClip = null;
    [SerializeField] private AudioClip buttonClickSound = null;

    [Header("References")]
    [SerializeField] private AudioMixer audioMixer = null;
    [SerializeField] private AudioSource musicAudioSource = null;
    [SerializeField] private AudioSource soundAudioSource = null;

    private const string AUDIO_MIXER_VOLUME_KEY = "Volume";
    private const string AUDIO_MIXER_MUSIC_VOLUME_KEY = "MusicVolume";
    private const string AUDIO_MIXER_SOUND_VOLUME_KEY = "SoundVolume";

    private const string IS_MUTED_KEY = "IsMuted";
    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    private const string SOUND_VOLUME_KEY = "SoundVolume";
    public bool IsMuted
    {
        get => PlayerPrefs.GetInt(IS_MUTED_KEY, 0) == 1;
        set
        {
            PlayerPrefs.SetInt(IS_MUTED_KEY, value ? 1 : 0);
            audioMixer.SetFloat(AUDIO_MIXER_VOLUME_KEY, value ? -80f : 0f);
        }
    }

    public int MusicVolume
    {
        get => PlayerPrefs.GetInt(MUSIC_VOLUME_KEY, 50);
        set 
        {
            PlayerPrefs.SetInt(MUSIC_VOLUME_KEY, value);
            audioMixer.SetFloat(AUDIO_MIXER_MUSIC_VOLUME_KEY, LinearToDecibel(value, 0, 100));
        }
    }

    public int SoundVolume
    {
        get => PlayerPrefs.GetInt(SOUND_VOLUME_KEY, 50);
        set 
        {
            PlayerPrefs.SetInt(SOUND_VOLUME_KEY, value);
            audioMixer.SetFloat(AUDIO_MIXER_SOUND_VOLUME_KEY, LinearToDecibel(value, 0, 100));
        }
    }

    public override void Initialize()
    {
        audioMixer.SetFloat(AUDIO_MIXER_VOLUME_KEY, IsMuted ? -80f : 0f);
        audioMixer.SetFloat(AUDIO_MIXER_MUSIC_VOLUME_KEY, LinearToDecibel(MusicVolume, 0, 100));
        audioMixer.SetFloat(AUDIO_MIXER_SOUND_VOLUME_KEY, LinearToDecibel(SoundVolume, 0, 100));

        PlayMusic();

        base.Initialize();
    }

    public void PlayMusic()
    {
        musicAudioSource.clip = musicClip;
        musicAudioSource.Play();
    }

    public void PlayButtonClickSound()
    {
        soundAudioSource.PlayOneShot(buttonClickSound);
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
