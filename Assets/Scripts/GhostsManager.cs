using System;
using System.Collections.Generic;
using UnityEngine;

public class GhostsManager : BaseGameManager
{
    [Header("Settings")]
    [SerializeField] private int maxGhostsCount = 5;
    [SerializeField] private float recordingDuration = 5f;

    private EchoData echoData = new EchoData();
    public int MaxGhostsCount => maxGhostsCount;
    public int GhostsCount { get; private set; } = 0;
    public bool IsRecording { get; private set; } = false;
    public bool IsRecorded {  get; private set; } = false;
    public bool IsPlaying { get; private set; } = false;
    public bool IsPlayed { get; private set; } = false;

    private Vector2 lastDirection = Vector2.zero;

    private Ghost ghost = null;
    private IMove iMove = null;
    private IJump iJump = null;
    private IShoot iShoot = null;
    private IActivator iActivator = null;

    public event Action<int> onGhostsCountChanged = null;
    public event Action<float, float> onRecordingStarted = null;
    public event Action onRecordingStopped = null;
    public event Action<float, float> onPlayingStarted = null;
    public event Action onPlayingStopped = null;

    private void Update()
    {
        CheckRecording();
        Play();
    }

    protected override void DeinitializeInternal()
    {
        base.DeinitializeInternal();

        onRecordingStarted = null;
        onRecordingStopped = null;
        onPlayingStarted = null;
        onPlayingStopped = null;
    }

    public void Setup(IMove iMove, IJump iJump, IShoot iShoot, IActivator iActivator)
    {
        if (this.iMove != null)
        {
            this.iMove.OnMove -= OnMove;
        }

        if (this.iJump != null)
        {
            this.iJump.OnJump -= OnJump;
        }

        if (this.iShoot != null)
        {
            this.iShoot.OnShoot -= OnShoot;
        }

        if (this.iActivator != null)
        {
            this.iActivator.OnActivate -= OnActivate;
        }

        this.iMove = iMove;
        this.iJump = iJump;
        this.iShoot = iShoot;
        this.iActivator = iActivator;

        if (this.iMove != null)
        {
            this.iMove.OnMove += OnMove;
        }

        if (this.iJump != null)
        {
            this.iJump.OnJump += OnJump;
        }

        if (this.iShoot != null)
        {
            this.iShoot.OnShoot += OnShoot;
        }

        if (this.iActivator != null)
        {
            this.iActivator.OnActivate += OnActivate;
        }
    }

    public void StartRecording()
    {
        if (IsRecording)
        {
            Debug.LogWarning("Already recording. Please stop the current recording before starting a new one.");
            return;
        }

        if (IsRecorded)
        {
            Debug.LogWarning("Already recorded. Please play the recording before starting a new one.");
            return;
        }

        if (GhostsCount >= maxGhostsCount)
        {
            Debug.LogWarning("Maximum number of ghosts reached. Cannot start a new recording.");
            return;
        }

        GhostsCount++;
        IsRecording = true;
        IsPlayed = false;

        echoData = new EchoData();
        echoData.recordPosition = Manager.Instance.GetManager<PlayerManager>().player.transform.position;
        echoData.recordFaceDirection = Manager.Instance.GetManager<PlayerManager>().player.iFace.FaceDirection;
        echoData.recordStartTime = Manager.Instance.GetManager<TimeManager>().GameTime;
        echoData.frames = new List<EchoFrameData>()
        {
            new EchoFrameData
            {
                time = Manager.Instance.GetManager<TimeManager>().GameTime,
                moveDirection = lastDirection,
            }
        };

        onRecordingStarted?.Invoke(recordingDuration, echoData.recordStartTime);
    }

    public void StopRecording()
    {
        if (!IsRecording)
        {
            Debug.LogWarning("Not currently recording. Please start a recording first.");
            return;
        }

        IsRecording = false;
        IsRecorded = true;

        echoData.recordStopTime = Manager.Instance.GetManager<TimeManager>().GameTime;

        onRecordingStopped?.Invoke();
    }

    public void PlayRecording()
    {
        if (IsRecording || !IsRecorded)
        {
            return;
        }

        IsRecorded = false;
        IsPlaying = true;
        echoData.playStartTime = Manager.Instance.GetManager<TimeManager>().GameTime;

        ObjectsPoolType ghostOPT = Manager.Instance.GetManager<ObjectsPoolsManager>().GhostOPT;
        GameObject ghostGo = Manager.Instance.GetManager<ObjectsPoolsManager>().GetPool(ghostOPT).Get();
        ghost = ghostGo.GetComponent<Ghost>();
        ghost.transform.SetParent(null);
        ghost.transform.SetPositionAndRotation(echoData.recordPosition, Quaternion.identity);
        if(ghost.TryGetComponent(out IFace iFace))
        {
            iFace.FaceDirection = echoData.recordFaceDirection;
        }

        onPlayingStarted?.Invoke(recordingDuration, echoData.playStartTime);
    }

    private void OnMove(Vector2 direction)
    {
        lastDirection = direction;

        if (!IsRecording)
            return;

        EchoFrameData frame = new EchoFrameData
        {
            time = Manager.Instance.GetManager<TimeManager>().GameTime,
            moveDirection = direction
        };

        echoData.frames.Add(frame);

        Debug.Log($"Recorded frame at time {frame.time}: Move Direction = {frame.moveDirection}");
    }

    private void OnJump()
    {
        if (!IsRecording)
            return;

        EchoFrameData frame = new EchoFrameData
        {
            time = Manager.Instance.GetManager<TimeManager>().GameTime,
            moveDirection = lastDirection,
            isJumping = true
        };

        echoData.frames.Add(frame);

        Debug.Log($"Recorded frame at time {frame.time}: Jump = {frame.isJumping}");
    }

    private void OnShoot()
    {
        if (!IsRecording)
            return;

        EchoFrameData frame = new EchoFrameData
        {
            time = Manager.Instance.GetManager<TimeManager>().GameTime,
            moveDirection = lastDirection,
            isShooting = true
        };

        echoData.frames.Add(frame);

        Debug.Log($"Recorded frame at time {frame.time}: Shoot = {frame.isShooting}");
    }

    private void OnActivate()
    {
        if (!IsRecording)
            return;

        EchoFrameData frame = new EchoFrameData
        {
            time = Manager.Instance.GetManager<TimeManager>().GameTime,
            moveDirection = lastDirection,
            isActivating = true
        };
        echoData.frames.Add(frame);

        Debug.Log($"Recorded frame at time {frame.time}: Activate");
    }

    private void CheckRecording()
    {
        if (!IsRecording)
            return;

        if (Manager.Instance.GetManager<TimeManager>().GameTime - echoData.recordStartTime >= recordingDuration)
        {
            StopRecording();
            Debug.Log("Recording duration reached. Stopping recording.");
        }
    }

    private void Play()
    {
        if(!IsPlaying)
        {
            return;
        }

        float offset = echoData.playStartTime - echoData.recordStartTime;
        if(echoData.recordStopTime < Manager.Instance.GetManager<TimeManager>().GameTime - offset)
        {
            IsPlaying = false;
            IsPlayed = true;

            if (ghost.TryGetComponent(out IPooledObject pooledObject))
            {
                pooledObject.Pool.Release(ghost.gameObject);
            }
            else
            {
                Destroy(ghost.gameObject);
            }
            ghost = null;

            onPlayingStopped?.Invoke();
        }
        else
        {
            for (int i = 0; i < echoData.frames.Count; i++)
            {
                if (echoData.frames[0].time < Manager.Instance.GetManager<TimeManager>().GameTime - offset)
                {
                    ghost.SetEchoFrameData(echoData.frames[0]);
                    echoData.frames.RemoveAt(0);
                    i--;
                }
            }
        }
    }
}
