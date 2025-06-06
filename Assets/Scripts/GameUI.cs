using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GameUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Button pauseButton = null;
        [SerializeField] private TextMeshProUGUI timeTMP = null;
        [Space]
        [SerializeField] private Slider recordingSlider = null;
        [SerializeField] private Slider playingSlider = null;
        [SerializeField] private Button toggleActionsRecordingButton = null;
        [SerializeField] private Image toggleActionsRecordingIconImage = null;
        [SerializeField] private Button playRecordedActionsButton = null;
        [SerializeField] private Image playRecordedActionsIconImage = null;
        [Space]
        [SerializeField] private GhostIndicators ghostIndicators = null;

        private float gameTime = 0f;

        private GhostsManager ghostsManager = null;
        private TimeManager timeManager = null;

        private void Awake()
        {
            ghostsManager = Manager.Instance.GetManager<GhostsManager>();
            timeManager = Manager.Instance.GetManager<TimeManager>();
        }

        private void OnEnable()
        {
            pauseButton.onClick.AddListener(OnPauseButtonClicked);

            toggleActionsRecordingButton.onClick.AddListener(OnToggleActionsRecordingButtonClicked);
            playRecordedActionsButton.onClick.AddListener(OnPlayRecordedActionsButtonClicked);

            ghostsManager.OnRecordingStarted += OnRecordingStarted;
            ghostsManager.OnRecordingStopped += OnRecordingStopped;
            ghostsManager.OnPlayingStarted += OnPlayingStarted;
            ghostsManager.OnPlayingStopped += OnPlayingStopped;
            timeManager.OnGameTimeUpdated += OnGameTimeUpdated;
        }

        private void Start()
        {
            RefreshRecordingSlider();
            RefreshPlayingSlider();
            RefreshToggleActionsRecordingButton();
            RefreshPlayRecordedActionsButton();

            ghostIndicators.Init();

            OnGameTimeUpdated(timeManager.GameTime);
        }

        private void Update()
        {
            RefreshRecordingSlider();
            RefreshPlayingSlider();
        }

        private void OnDisable()
        {
            pauseButton.onClick.RemoveListener(OnPauseButtonClicked);

            toggleActionsRecordingButton.onClick.RemoveListener(OnToggleActionsRecordingButtonClicked);
            playRecordedActionsButton.onClick.RemoveListener(OnPlayRecordedActionsButtonClicked);

            ghostsManager.OnRecordingStarted -= OnRecordingStarted;
            ghostsManager.OnRecordingStopped -= OnRecordingStopped;
            ghostsManager.OnPlayingStarted -= OnPlayingStarted;
            ghostsManager.OnPlayingStopped -= OnPlayingStopped;
            timeManager.OnGameTimeUpdated -= OnGameTimeUpdated;
        }

        private void OnPauseButtonClicked()
        {
            Debug.Log("Pause button clicked");

            Manager.Instance.GetManager<AudioManager>().PlayButtonClickSound();

            Manager.Instance.GetManager<PopupsManager>().GetPopup<PopupPause>().Show();
        }

        private void OnToggleActionsRecordingButtonClicked()
        {
            Debug.Log("Toggle Actions Recording button clicked");

            Manager.Instance.GetManager<AudioManager>().PlayButtonClickSound();

            if (ghostsManager.IsRecording)
            {
                ghostsManager.StopRecording();
            }
            else
            {
                ghostsManager.StartRecording();
            }
        }

        private void OnPlayRecordedActionsButtonClicked()
        {
            Debug.Log("Play Recorded Actions button clicked");

            Manager.Instance.GetManager<AudioManager>().PlayButtonClickSound();

            ghostsManager.PlayRecording();
        }

        private void OnGameTimeUpdated(float gameTime)
        {
            this.gameTime = gameTime;
            RefreshTimeTMP();
        }

        private void OnRecordingStarted(float recordingDuration, float recordingTime)
        {
            ResetSliders();

            recordingSlider.gameObject.SetActive(true);
            recordingSlider.maxValue = recordingTime + recordingDuration;
            recordingSlider.minValue = recordingTime;
            recordingSlider.value = recordingTime;

            RefreshToggleActionsRecordingButton();
            RefreshPlayRecordedActionsButton();
        }

        private void OnRecordingStopped()
        {
            //recordingSlider.gameObject.SetActive(false);

            RefreshToggleActionsRecordingButton();
            RefreshPlayRecordedActionsButton();
        }

        private void OnPlayingStarted(float recordingDuration, float recordingTime)
        {
            playingSlider.gameObject.SetActive(true);
            playingSlider.maxValue = recordingTime + recordingDuration;
            playingSlider.minValue = recordingTime;
            playingSlider.value = recordingTime;

            RefreshToggleActionsRecordingButton();
            RefreshPlayRecordedActionsButton();
        }

        private void OnPlayingStopped()
        {
            //playingSlider.gameObject.SetActive(false);

            RefreshToggleActionsRecordingButton();
            RefreshPlayRecordedActionsButton();
        }

        private void ResetSliders()
        {
            recordingSlider.gameObject.SetActive(false);
            recordingSlider.maxValue = 0f;
            recordingSlider.minValue = 0f;
            recordingSlider.value = 0f;


            playingSlider.gameObject.SetActive(false);
            playingSlider.maxValue = 0f;
            playingSlider.minValue = 0f;
            playingSlider.value = 0f;
        }

        private void RefreshTimeTMP()
        {
            int totalSeconds = Mathf.FloorToInt(gameTime);
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;

            timeTMP.text = $"{minutes}:{seconds:D2}";
        }

        private void RefreshRecordingSlider()
        {
            if (!ghostsManager.IsRecording)
                return;

            recordingSlider.value = Manager.Instance.GetManager<TimeManager>().GameTime;
        }

        private void RefreshPlayingSlider()
        {
            if (!ghostsManager.IsPlaying)
                return;

            playingSlider.value = Manager.Instance.GetManager<TimeManager>().GameTime;
        }

        private void RefreshToggleActionsRecordingButton()
        {
            if (ghostsManager.IsRecorded
                || ghostsManager.IsPlaying)
            {
                toggleActionsRecordingButton.interactable = false;
            }
            else
            {
                toggleActionsRecordingButton.interactable = true;
            }

            if (ghostsManager.IsRecording)
            {
                toggleActionsRecordingIconImage.sprite =
                    Manager.Instance.GetManager<SpriteAtlasesManager>().GetUiSprite(SpriteAtlasesManager.UI_SPRITE_ICON_STOP_NAME);
            }
            else
            {
                toggleActionsRecordingIconImage.sprite =
                    Manager.Instance.GetManager<SpriteAtlasesManager>().GetUiSprite(SpriteAtlasesManager.UI_SPRITE_ICON_RECORD_NAME);
            }
        }

        private void RefreshPlayRecordedActionsButton()
        {
            if (!ghostsManager.IsRecorded
                || ghostsManager.IsPlaying || ghostsManager.IsPlayed)
            {
                playRecordedActionsButton.interactable = false;
            }
            else
            {
                playRecordedActionsButton.interactable = true;
            }

            playRecordedActionsIconImage.sprite =
                Manager.Instance.GetManager<SpriteAtlasesManager>().GetUiSprite(SpriteAtlasesManager.UI_SPRITE_ICON_PLAY_NAME);
        }
    }
}
