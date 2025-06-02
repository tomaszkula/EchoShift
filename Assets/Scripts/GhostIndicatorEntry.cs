using UnityEngine;
using UnityEngine.UI;

public class GhostIndicatorEntry : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Color recordingColor = new Color(0f, 0.75f, 0f);
    [SerializeField] private Color recordedColor = new Color(1f, 1f, 0f);
    [SerializeField] private Color playingColor = new Color(0f, 0.75f, 1f);
    [SerializeField] private Color playedColor = new Color(0.5f, 0.5f, 0.5f);

    [Header("References")]
    [SerializeField] private Image iconImage = null;

    private bool _isRecording = false;
    public bool isRecording
    {
        get => _isRecording;
        set
        {
            if (_isRecording != value)
            {
                _isRecording = value;
                RefreshIcon();
            }
        }
    }

    private bool _isRecorded = false;
    public bool isRecorded
    {
        get => _isRecorded;
        set
        {
            if (_isRecorded != value)
            {
                _isRecorded = value;
                RefreshIcon();
            }
        }
    }

    private bool _isPlaying = false;
    public bool isPlaying
    {
        get => _isPlaying;
        set
        {
            if (_isPlaying != value)
            {
                _isPlaying = value;
                RefreshIcon();
            }
        }
    }

    private bool _isPlayed = false;
    public bool isPlayed
    {
        get => _isPlayed;
        set
        {
            if (_isPlayed != value)
            {
                _isPlayed = value;
                RefreshIcon();
            }
        }
    }

    public void Init()
    {
        _isRecording = false;
        _isPlaying = false;
        _isPlayed = false;

        RefreshIcon();
    }

    private void RefreshIcon()
    {
        if(isRecording)
        {
            iconImage.color = recordingColor;
        }
        else if(isRecorded)
        {
            iconImage.color = recordedColor;
        }
        else if(isPlaying)
        {
            iconImage.color = playingColor;
        }
        else if(isPlayed)
        {
            iconImage.color = playedColor;
        }
        else
        {
            iconImage.color = Color.white;
        }
    }
}
