using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class LevelEntry : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button button = null;
    [SerializeField] private TextMeshProUGUI labelTMP = null;
    [SerializeField] private Transform starsContainer = null;

    private List<LevelStarEntry> starEntries = new List<LevelStarEntry>();

    public LevelData CurrentData {  get; private set; }

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
        Debug.Log($"Level selected: {CurrentData.LevelId}");

        Manager.Instance.GetManager<AudioManager>().PlayButtonClickSound();

        await Manager.Instance.GetManager<LevelsManager>().Load(CurrentData);
    }

    public void Init(LevelData data)
    {
        CurrentData = data;

        RefreshLabelTMP();
        RefreshStars();
    }

    private void RefreshLabelTMP()
    {
        labelTMP.text = $"Level {CurrentData.LevelId}";
    }

    private void RefreshStars()
    {
        ObjectsPoolType starEntryOPT = Manager.Instance.GetManager<ObjectsPoolsManager>().MainMenuLevelStarEntryOPT;
        ObjectPool<GameObject> starEntryOP = Manager.Instance.GetManager<ObjectsPoolsManager>().GetPool(starEntryOPT);

        for (int i = 0; i < starEntries.Count; i++)
        {
            starEntryOP.Release(starEntries[i].gameObject);
        }

        for (int i = 0; i < CurrentData.MaxStars; i++)
        {
            GameObject starEntryGo = Manager.Instance.GetManager<ObjectsPoolsManager>().GetPool(starEntryOPT).Get();
            starEntryGo.transform.SetParent(starsContainer, false);

            LevelStarEntry starEntry = starEntryGo.GetComponent<LevelStarEntry>();
            LevelStarEntry.Data data = new LevelStarEntry.Data
            {
                Id = i,
                IsUnlocked = i < CurrentData.Stars
            };
            starEntry.Init(data);
            starEntries.Add(starEntry);
        }
    }
}
