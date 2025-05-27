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
        [SerializeField] private Button startRecordingButton = null;
        [SerializeField] private Button playRecordingButton = null;

        private float gameTime = 0f;
        private bool isRecording = false;
        private bool isPlaying = false;

        private void OnEnable()
        {
            pauseButton.onClick.AddListener(OnPauseButtonClicked);
            volumeButton.onClick.AddListener(OnVolumeButtonClicked);

            startRecordingButton.onClick.AddListener(OnStartRecordingButtonClicked);
            playRecordingButton.onClick.AddListener(OnPlayRecordingButtonClicked);

            TryInitGameManagerEvents();
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

            startRecordingButton.onClick.RemoveListener(OnStartRecordingButtonClicked);
            playRecordingButton.onClick.RemoveListener(OnPlayRecordingButtonClicked);

            DeinitGameManagerEvents();
        }

        private void OnPauseButtonClicked()
        {
            Debug.Log("Pause button clicked");

            ManagersController.Instance.GetManager<AudioManager>().PlayButtonClickSound();
            ManagersController.Instance.GetManager<ScenesManager>().LoadScene(ScenesManager.MAIN_MENU_SCENE_NAME);
        }

        private void OnVolumeButtonClicked()
        {
            Debug.Log("Volume button clicked");

            ManagersController.Instance.GetManager<AudioManager>().PlayButtonClickSound();

            ManagersController.Instance.GetManager<AudioManager>().IsMuted = !ManagersController.Instance.GetManager<AudioManager>().IsMuted;
        }

        private void OnStartRecordingButtonClicked()
        {
            Debug.Log("Start Recording button clicked");

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

        private void OnPlayRecordingButtonClicked()
        {
            Debug.Log("Play Recording button clicked");

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
            if (GameManager.IsInitialized)
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

            isRecording = true;

            recordingSlider.gameObject.SetActive(true);
            recordingSlider.maxValue = recordingTime + recordingDuration;
            recordingSlider.minValue = recordingTime;
            recordingSlider.value = recordingTime;
        }

        private void OnRecordingStopped()
        {
            isRecording = false;

            //recordingSlider.gameObject.SetActive(false);
        }

        private void OnPlayingStarted(float recordingDuration, float recordingTime)
        {
            isPlaying = true;

            playingSlider.gameObject.SetActive(true);
            playingSlider.maxValue = recordingTime + recordingDuration;
            playingSlider.minValue = recordingTime;
            playingSlider.value = recordingTime;
        }

        private void OnPlayingStopped()
        {
            isPlaying = false;

            //playingSlider.gameObject.SetActive(false);
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
            if (!isRecording)
                return;

            recordingSlider.value = GameManager.Instance.GameTime;
        }

        private void RefreshPlayingSlider()
        {
            if (!isPlaying)
                return;

            playingSlider.value = GameManager.Instance.GameTime;
        }
    }
}
