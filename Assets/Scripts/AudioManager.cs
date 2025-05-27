using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : BaseManager
{
    [Header("Settings")]
    [SerializeField] private AudioClip buttonClickSound = null;

    [Header("References")]
    [SerializeField] private AudioMixer audioMixer = null;
    [SerializeField] private AudioSource audioSource = null;

    private const string AUDIO_MIXER_VOLUME_KEY = "Volume";

    private const string IS_MUTED_KEY = "IsMuted";
    public bool IsMuted
    {
        get => PlayerPrefs.GetInt(IS_MUTED_KEY, 0) == 1;
        set
        {
            PlayerPrefs.SetInt(IS_MUTED_KEY, value ? 1 : 0);
            audioMixer.SetFloat(AUDIO_MIXER_VOLUME_KEY, value ? -80f : 0f);
        }
    }

    public override void Initialize()
    {
        audioMixer.SetFloat(AUDIO_MIXER_VOLUME_KEY, IsMuted ? -80f : 0f);

        base.Initialize();
    }

    public void PlayButtonClickSound()
    {
        audioSource.PlayOneShot(buttonClickSound);
    }
}
