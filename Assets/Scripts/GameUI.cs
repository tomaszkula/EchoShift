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

        [SerializeField] private Button startRecordingButton = null;
        [SerializeField] private Button playRecordingButton = null;

        private float gameTime = 0f;

        private void OnEnable()
        {
            pauseButton.onClick.AddListener(OnPauseButtonClicked);
            volumeButton.onClick.AddListener(OnVolumeButtonClicked);

            startRecordingButton.onClick.AddListener(OnStartRecordingButtonClicked);
            playRecordingButton.onClick.AddListener(OnPlayRecordingButtonClicked);

            TryInitGameManagerEvents();
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
        }

        private void OnStartRecordingButtonClicked()
        {
            Debug.Log("Start Recording button clicked");

            ManagersController.Instance.GetManager<AudioManager>().PlayButtonClickSound();

            GameManager.Instance.GetManager<GhostsManager>().StartRecording();
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

            OnGameTimeUpdated(GameManager.Instance.GameTime);
        }

        private void DeinitGameManagerEvents()
        {
            GameManager.Instance.OnGameTimeUpdated -= OnGameTimeUpdated;
        }

        private void OnGameTimeUpdated(float gameTime)
        {
            this.gameTime = gameTime;
            RefreshTimeTMP();
        }

        private void RefreshTimeTMP()
        {
            int totalSeconds = Mathf.FloorToInt(gameTime);
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;

            timeTMP.text = $"{minutes}:{seconds:D2}";
        }
    }
}
