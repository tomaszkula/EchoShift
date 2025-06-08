using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelEntry : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button button = null;
    [SerializeField] private TextMeshProUGUI labelTMP = null;

    public LevelData currentData {  get; private set; }

    private void OnEnable()
    {
        button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnButtonClicked);
    }

    private async void OnButtonClicked()
    {
        Debug.Log($"Level selected: {currentData.LevelName}");

        Manager.Instance.GetManager<AudioManager>().PlayButtonClickSound();

        await Manager.Instance.GetManager<LevelsManager>().Load(currentData);
    }

    public void Init(LevelData data)
    {
        currentData = data;

        RefreshLabelTMP();
    }

    private void RefreshLabelTMP()
    {
        labelTMP.text = currentData.LevelName;
    }
}
