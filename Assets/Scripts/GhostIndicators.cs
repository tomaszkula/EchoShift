using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class GhostIndicators : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject ghostIndicatorEntriesContainer = null;

    private List<GhostIndicatorEntry> ghostIndicatorEntries = new List<GhostIndicatorEntry>();

    private async void OnEnable()
    {
        await new WaitUntil(() => GameManager.IsInitialized);

        GameManager.Instance.GetManager<GhostsManager>().onRecordingStarted += OnRecordingStarted;
        GameManager.Instance.GetManager<GhostsManager>().onRecordingStopped += OnRecordingStopped;
        GameManager.Instance.GetManager<GhostsManager>().onPlayingStarted += OnPlayingStarted;
        GameManager.Instance.GetManager<GhostsManager>().onPlayingStopped += OnPlayingStopped;
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null
            && GameManager.Instance.GetManager<GhostsManager>().IsInitialized)
        {
            GameManager.Instance.GetManager<GhostsManager>().onRecordingStarted -= OnRecordingStarted;
            GameManager.Instance.GetManager<GhostsManager>().onRecordingStopped -= OnRecordingStopped;
            GameManager.Instance.GetManager<GhostsManager>().onPlayingStarted -= OnPlayingStarted;
            GameManager.Instance.GetManager<GhostsManager>().onPlayingStopped -= OnPlayingStopped;
        }
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
        for (int i = 0; i < GameManager.Instance.GetManager<GhostsManager>().MaxGhostsCount; i++)
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
        int ghostId = GameManager.Instance.GetManager<GhostsManager>().GhostsCount - 1;
        ghostIndicatorEntries[ghostId].isRecording = GameManager.Instance.GetManager<GhostsManager>().IsRecording;
        ghostIndicatorEntries[ghostId].isRecorded = GameManager.Instance.GetManager<GhostsManager>().IsRecorded;
        ghostIndicatorEntries[ghostId].isPlaying = GameManager.Instance.GetManager<GhostsManager>().IsPlaying;
        ghostIndicatorEntries[ghostId].isPlayed = GameManager.Instance.GetManager<GhostsManager>().IsPlayed;
    }
}
