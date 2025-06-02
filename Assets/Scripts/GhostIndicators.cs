using System.Collections.Generic;
using UnityEngine;

public class GhostIndicators : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject ghostIndicatorEntriesContainer = null;

    private List<GhostIndicatorEntry> ghostIndicatorEntries = new List<GhostIndicatorEntry>();

    private void OnEnable()
    {
        TryInitGameManagerEvents();
    }

    private void OnDisable()
    {
        if(GameManager.Instance == null
            || !GameManager.Instance.GetManager<GhostsManager>().IsInitialized)
            return;

        GameManager.Instance.GetManager<GhostsManager>().onRecordingStarted -= OnRecordingStarted;
        GameManager.Instance.GetManager<GhostsManager>().onRecordingStopped -= OnRecordingStopped;
        GameManager.Instance.GetManager<GhostsManager>().onPlayingStarted -= OnPlayingStarted;
        GameManager.Instance.GetManager<GhostsManager>().onPlayingStopped -= OnPlayingStopped;
    }

    private void OnRecordingStarted(float duration, float startTime)
    {
        RefreshGhostIndicatorEntries();
    }

    private void OnRecordingStopped()
    {
        RefreshGhostIndicatorEntries();
    }

    private void OnPlayingStarted(float duration, float startTime)
    {
        RefreshGhostIndicatorEntries();
    }

    private void OnPlayingStopped()
    {
        RefreshGhostIndicatorEntries();
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
        GameManager.Instance.GetManager<GhostsManager>().onRecordingStarted += OnRecordingStarted;
        GameManager.Instance.GetManager<GhostsManager>().onRecordingStopped += OnRecordingStopped;
        GameManager.Instance.GetManager<GhostsManager>().onPlayingStarted += OnPlayingStarted;
        GameManager.Instance.GetManager<GhostsManager>().onPlayingStopped += OnPlayingStopped;
    }

    public void Init()
    {
        for (int i = 0; i < GameManager.Instance.GetManager<GhostsManager>().maxGhostsCount; i++)
        {
            GameObject ghostIndicatorEntryGo = GameManager.Instance.GetManager<ObjectPoolsManager>().GetPool(ObjectPoolsManager.PoolType.GhostIndicatorEntry).Get();
            ghostIndicatorEntryGo.transform.SetParent(ghostIndicatorEntriesContainer.transform, false);
            if (ghostIndicatorEntryGo.TryGetComponent(out GhostIndicatorEntry ghostIndicatorEntry))
            {
                ghostIndicatorEntry.Init();

                ghostIndicatorEntries.Add(ghostIndicatorEntry);
            }
        }
    }

    private void RefreshGhostIndicatorEntries()
    {
        int ghostId = GameManager.Instance.GetManager<GhostsManager>().ghostsCount - 1;
        ghostIndicatorEntries[ghostId].isRecording = GameManager.Instance.GetManager<GhostsManager>().isRecording;
        ghostIndicatorEntries[ghostId].isRecorded = GameManager.Instance.GetManager<GhostsManager>().isRecorded;
        ghostIndicatorEntries[ghostId].isPlaying = GameManager.Instance.GetManager<GhostsManager>().isPlaying;
        ghostIndicatorEntries[ghostId].isPlayed = GameManager.Instance.GetManager<GhostsManager>().isPlayed;
    }
}
