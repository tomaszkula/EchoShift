using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class GhostIndicators : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject ghostIndicatorEntriesContainer = null;

    private List<GhostIndicatorEntry> ghostIndicatorEntries = new List<GhostIndicatorEntry>();

    private GhostsManager ghostsManager = null;

    private void Awake()
    {
        ghostsManager = Manager.Instance.GetManager<GhostsManager>();
    }

    private void OnEnable()
    {
        ghostsManager.OnRecordingStarted += OnRecordingStarted;
        ghostsManager.OnRecordingStopped += OnRecordingStopped;
        ghostsManager.OnPlayingStarted += OnPlayingStarted;
        ghostsManager.OnPlayingStopped += OnPlayingStopped;
    }

    private void OnDisable()
    {
        ghostsManager.OnRecordingStarted -= OnRecordingStarted;
        ghostsManager.OnRecordingStopped -= OnRecordingStopped;
        ghostsManager.OnPlayingStarted -= OnPlayingStarted;
        ghostsManager.OnPlayingStopped -= OnPlayingStopped;
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

    public void Init()
    {
        for (int i = 0; i < ghostsManager.MaxGhostsCount; i++)
        {
            ObjectsPoolType uiGhostIndicatorEntryOPT = Manager.Instance.GetManager<ObjectsPoolsManager>().UiGhostIndicatorEntryOPT;
            GameObject ghostIndicatorEntryGo = Manager.Instance.GetManager<ObjectsPoolsManager>().GetPool(uiGhostIndicatorEntryOPT).Get();
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
        int ghostId = ghostsManager.GhostsCount - 1;
        ghostIndicatorEntries[ghostId].isRecording = ghostsManager.IsRecording;
        ghostIndicatorEntries[ghostId].isRecorded = ghostsManager.IsRecorded;
        ghostIndicatorEntries[ghostId].isPlaying = ghostsManager.IsPlaying;
        ghostIndicatorEntries[ghostId].isPlayed = ghostsManager.IsPlayed;
    }
}
