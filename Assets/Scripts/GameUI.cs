using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private Button pauseButton;
        [SerializeField] private TextMeshProUGUI timeTMP;
        [SerializeField] private Button volumeButton;

        private float gameTime = 0f;

        private void OnEnable()
        {
            pauseButton.onClick.AddListener(OnPauseButtonClicked);
            volumeButton.onClick.AddListener(OnVolumeButtonClicked);

            TryInitGameManagerEvents();
        }

        private void OnDisable()
        {
            pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
            volumeButton.onClick.RemoveListener(OnVolumeButtonClicked);

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
