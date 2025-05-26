using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton = null;

    private void OnEnable()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(OnPlayButtonClicked);
    }

    private void OnPlayButtonClicked()
    {
        Debug.Log("Play button clicked! Starting game...");

        ManagersController.Instance.GetManager<AudioManager>().PlayButtonClickSound();
        ManagersController.Instance.GetManager<ScenesManager>().LoadScene(ScenesManager.GAME_SCENE_NAME);
    }
}
