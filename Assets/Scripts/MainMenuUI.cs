using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("References")]
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

        Manager.Instance.GetManager<AudioManager>().PlayButtonClickSound();

        Manager.Instance.GetManager<LevelsManager>().Load();
    }
}
