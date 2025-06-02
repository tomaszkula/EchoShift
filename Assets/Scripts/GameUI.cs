using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private Button pauseButton = null;
        [SerializeField] private TextMeshProUGUI timeTMP = null;
        [SerializeField] private Button volumeButton = null;

        [SerializeField] private Slider recordingSlider = null;
        [SerializeField] private Slider playingSlider = null;
        [SerializeField] private Button toggleActionsRecordingButton = null;
        [SerializeField] private Image toggleActionsRecordingIconImage = null;
        [SerializeField] private Button playRecordedActionsButton = null;
        [SerializeField] private Image playRecordedActionsIconImage = null;

        private float gameTime = 0f;

        private void OnEnable()
        {
            pauseButton.onClick.AddListener(OnPauseButtonClicked);
            volumeButton.onClick.AddListener(OnVolumeButtonClicked);

            toggleActionsRecordingButton.onClick.AddListener(OnToggleActionsRecordingButtonClicked);
            playRecordedActionsButton.onClick.AddListener(OnPlayRecordedActionsButtonClicked);

            TryInitGameManagerEvents();
        }

        private void Start()
        {
            RefreshRecordingSlider();
            RefreshPlayingSlider();
            RefreshToggleActionsRecordingButton();
            RefreshPlayRecordedActionsButton();
        }

        private void Update()
        {
            RefreshRecordingSlider();
            RefreshPlayingSlider();
        }

        private void OnDisable()
        {
            pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
            volumeButton.onClick.RemoveListener(OnVolumeButtonClicked);

            toggleActionsRecordingButton.onClick.RemoveListener(OnToggleActionsRecordingButtonClicked);
            playRecordedActionsButton.onClick.RemoveListener(OnPlayRecordedActionsButtonClicked);

            DeinitGameManagerEvents();
        }

        private void OnPauseButtonClicked()
        {
            Debug.Log("Pause button clicked");

            ManagersController.Instance.GetManager<AudioManager>().PlayButtonClickSound();

            GameManager.Instance.GetManager<PopupsManager>().GetPopup<PopupSettings>().Show();
        }

        private void OnVolumeButtonClicked()
        {
            Debug.Log("Volume button clicked");

            ManagersController.Instance.GetManager<AudioManager>().PlayButtonClickSound();

            ManagersController.Instance.GetManager<AudioManager>().IsMuted = !ManagersController.Instance.GetManager<AudioManager>().IsMuted;
        }

        private void OnToggleActionsRecordingButtonClicked()
        {
            Debug.Log("Toggle Actions Recording button clicked");

            ManagersController.Instance.GetManager<AudioManager>().PlayButtonClickSound();

            if (GameManager.Instance.GetManager<GhostsManager>().isRecording)
            {
                GameManager.Instance.GetManager<GhostsManager>().StopRecording();
            }
            else
            {
                GameManager.Instance.GetManager<GhostsManager>().StartRecording();
            }
        }

        private void OnPlayRecordedActionsButtonClicked()
        {
            Debug.Log("Play Recorded Actions button clicked");

            ManagersController.Instance.GetManager<AudioManager>().PlayButtonClickSound();

            GameManager.Instance.GetManager<GhostsManager>().PlayRecording();
        }

        private void TryInitGameManagerEvents()
        {
            if (GameManager.IsInitialized)
            {
                InitGameManagerEvents();
            }
            else
            {
                GameManager.OnInitialized += InitGameManagerEvents;
            }
        }

        private void InitGameManagerEvents()
        {
            GameManager.Instance.OnGameTimeUpdated += OnGameTimeUpdated;
            GameManager.Instance.GetManager<GhostsManager>().onRecordingStarted += OnRecordingStarted;
            GameManager.Instance.GetManager<GhostsManager>().onRecordingStopped += OnRecordingStopped;
            GameManager.Instance.GetManager<GhostsManager>().onPlayingStarted += OnPlayingStarted;
            GameManager.Instance.GetManager<GhostsManager>().onPlayingStopped += OnPlayingStopped;

            OnGameTimeUpdated(GameManager.Instance.GameTime);
        }

        private void DeinitGameManagerEvents()
        {
            if (GameManager.IsInitialized &&
                GameManager.Instance.GetManager<GhostsManager>().isInitialized)
            {
                GameManager.Instance.OnGameTimeUpdated -= OnGameTimeUpdated;
                GameManager.Instance.GetManager<GhostsManager>().onRecordingStarted -= OnRecordingStarted;
                GameManager.Instance.GetManager<GhostsManager>().onRecordingStopped -= OnRecordingStopped;
                GameManager.Instance.GetManager<GhostsManager>().onPlayingStarted -= OnPlayingStarted;
                GameManager.Instance.GetManager<GhostsManager>().onPlayingStopped -= OnPlayingStopped;
            }
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
            if (!GameManager.Instance.GetManager<GhostsManager>().isRecording)
                return;

            recordingSlider.value = GameManager.Instance.GameTime;
        }

        private void RefreshPlayingSlider()
        {
            if (!GameManager.Instance.GetManager<GhostsManager>().isPlaying)
                return;

            playingSlider.value = GameManager.Instance.GameTime;
        }

        private void RefreshToggleActionsRecordingButton()
        {
            if (GameManager.Instance.GetManager<GhostsManager>().isPlaying)
            {
                toggleActionsRecordingButton.interactable = false;
            }
            else
            {
                toggleActionsRecordingButton.interactable = true;
            }

            if (GameManager.Instance.GetManager<GhostsManager>().isRecording)
            {
                toggleActionsRecordingIconImage.sprite =
                    ManagersController.Instance.GetManager<SpriteAtlasesManager>().GetUiSprite(SpriteAtlasesManager.UI_SPRITE_ICON_STOP_NAME);
            }
            else
            {
                toggleActionsRecordingIconImage.sprite =
                    ManagersController.Instance.GetManager<SpriteAtlasesManager>().GetUiSprite(SpriteAtlasesManager.UI_SPRITE_ICON_RECORD_NAME);
            }
        }

        private void RefreshPlayRecordedActionsButton()
        {
            if (GameManager.Instance.GetManager<GhostsManager>().isRecording)
            {
                playRecordedActionsButton.interactable = false;
            }
            else
            {
                playRecordedActionsButton.interactable = true;
            }

            playRecordedActionsIconImage.sprite =
                ManagersController.Instance.GetManager<SpriteAtlasesManager>().GetUiSprite(SpriteAtlasesManager.UI_SPRITE_ICON_PLAY_NAME);
        }
    }
}
