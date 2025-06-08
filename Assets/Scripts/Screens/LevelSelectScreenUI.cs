using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectScreenUI : BaseScreen
{
    [Header("Settings")]
    [SerializeField] private LevelEntry levelEntryPrefab = null;

    [Header("References")]
    [SerializeField] private GameObject levelEntriesContainer = null;
    [SerializeField] private Button backButton = null;

    private List<LevelEntry> levelEntries = new List<LevelEntry>();

    private void OnEnable()
    {
        backButton.onClick.AddListener(OnBackButtonClicked);
    }

    private void OnDisable()
    {
        backButton.onClick.RemoveListener(OnBackButtonClicked);
    }

    private void OnBackButtonClicked()
    {
        Debug.Log("Back button clicked! Returning to main menu...");

        Manager.Instance.GetManager<AudioManager>().PlayButtonClickSound();

        mainMenuUI.SelectScreen(MainMenuUI.ScreenType.MainMenu);
    }

    public override void Show()
    {
        base.Show();

        Init();
    }

    private void Init()
    {
        ClearLevelEntries();
        InitLevelEntries();
    }

    private void ClearLevelEntries()
    {
        for(int i = 0; i < levelEntries.Count; i++)
        {
            Destroy(levelEntries[i].gameObject);
        }
        levelEntries.Clear();
    }

    private void InitLevelEntries()
    {
        for (int i = 0; i < Manager.Instance.GetManager<LevelsManager>().LevelsData.Count; i++)
        {
            LevelEntry levelEntry = Instantiate(levelEntryPrefab, levelEntriesContainer.transform);
            levelEntry.Init(Manager.Instance.GetManager<LevelsManager>().LevelsData[i]);

            levelEntries.Add(levelEntry);
        }
    }
}
