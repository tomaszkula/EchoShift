using System;
using System.Collections.Generic;
using UnityEngine;

public class GhostsManager : BaseManager
{
    [Header("Settings")]
    [SerializeField] private int maxGhostsCount = 5;
    [SerializeField] private float recordingDuration = 5f;

    private EchoData echoData = new EchoData();
    private Vector2 lastMoveDirection = Vector2.zero;
    private Vector2 lastClimbDirection = Vector2.zero;

    private Ghost ghost = null;
    private IMove iMove = null;
    private IJump iJump = null;
    private IShoot iShoot = null;
    private IClimb iClimb = null;
    private IActivator iActivator = null;

    private PlayerManager playerManager = null;
    private TimeManager timeManager = null;

    public int MaxGhostsCount => maxGhostsCount;
    public float RecordingDuration => recordingDuration;
    public int GhostsCount { get; private set; } = 0;
    public bool IsRecording { get; private set; } = false;
    public bool IsRecorded { get; private set; } = false;
    public bool IsPlaying { get; private set; } = false;
    public bool IsPlayed { get; private set; } = false;

    public event Action<int> OnGhostsCountChanged = null;
    public event Action<float, float> OnRecordingStarted = null;
    public event Action OnRecordingStopped = null;
    public event Action<float, float> OnPlayingStarted = null;
    public event Action OnPlayingStopped = null;

    private void Update()
    {
        CheckRecording();
        Play();
    }

    protected override void InitializeInternal()
    {
        base.InitializeInternal();

        ResetManager();

        playerManager = Manager.Instance.GetManager<PlayerManager>();
        timeManager = Manager.Instance.GetManager<TimeManager>();
    }

    protected override void DeinitializeInternal()
    {
        base.DeinitializeInternal();

        OnGhostsCountChanged = null;
        OnRecordingStarted = null;
        OnRecordingStopped = null;
        OnPlayingStarted = null;
        OnPlayingStopped = null;
    }

    public void ResetManager()
    {
        GhostsCount = 0;
        IsRecording = false;
        IsRecorded = false;
        IsPlaying = false;
        IsPlayed = false;
    }

    public void Setup(IMove iMove, IJump iJump, IShoot iShoot, IClimb iClimb, IActivator iActivator)
    {
        if (this.iMove != null)
            this.iMove.OnMove -= OnMove;

        if (this.iJump != null)
            this.iJump.OnJump -= OnJump;

        if (this.iShoot != null)
            this.iShoot.OnShoot -= OnShoot;

        if (this.iClimb != null)
            this.iClimb.OnClimb -= OnClimb;

        if (this.iActivator != null)
            this.iActivator.OnActivate -= OnActivate;

        this.iMove = iMove;
        this.iJump = iJump;
        this.iShoot = iShoot;
        this.iClimb = iClimb;
        this.iActivator = iActivator;

        if (this.iMove != null)
            this.iMove.OnMove += OnMove;

        if (this.iJump != null)
            this.iJump.OnJump += OnJump;

        if (this.iShoot != null)
            this.iShoot.OnShoot += OnShoot;

        if (this.iClimb != null)
            this.iClimb.OnClimb += OnClimb;

        if (this.iActivator != null)
            this.iActivator.OnActivate += OnActivate;
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
        echoData.recordPosition = playerManager.Player.transform.position;
        echoData.recordFaceDirection = playerManager.Player.IFace.FaceDirection;
        echoData.recordStartTime = timeManager.GameTime;
        echoData.frames = new List<EchoFrameData>()
        {
            new EchoFrameData
            {
                time = timeManager.GameTime,
                moveDirection = lastMoveDirection,
                climbDirection = lastClimbDirection,
            }
        };

        OnRecordingStarted?.Invoke(recordingDuration, echoData.recordStartTime);
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

        echoData.recordStopTime = timeManager.GameTime;

        OnRecordingStopped?.Invoke();
    }

    public void PlayRecording()
    {
        if (IsRecording || !IsRecorded)
        {
            return;
        }

        IsRecorded = false;
        IsPlaying = true;
        echoData.playStartTime = timeManager.GameTime;

        ObjectsPoolType ghostOPT = Manager.Instance.GetManager<ObjectsPoolsManager>().GhostOPT;
        GameObject ghostGo = Manager.Instance.GetManager<ObjectsPoolsManager>().GetPool(ghostOPT).Get();
        ghost = ghostGo.GetComponent<Ghost>();
        ghost.transform.SetParent(null);
        ghost.transform.SetPositionAndRotation(echoData.recordPosition, Quaternion.identity);
        if(ghost.TryGetComponent(out IFace iFace))
        {
            iFace.FaceDirection = echoData.recordFaceDirection;
        }

        OnPlayingStarted?.Invoke(recordingDuration, echoData.playStartTime);
    }

    private void OnMove(Vector2 direction)
    {
        lastMoveDirection = direction;

        if (!IsRecording)
            return;

        EchoFrameData frame = new EchoFrameData
        {
            time = timeManager.GameTime,
            moveDirection = direction,
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
            time = timeManager.GameTime,
            moveDirection = lastMoveDirection,
            isJumping = true,
            climbDirection = lastClimbDirection,
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
            time = timeManager.GameTime,
            moveDirection = lastMoveDirection,
            isShooting = true,
            climbDirection = lastClimbDirection,
        };

        echoData.frames.Add(frame);

        Debug.Log($"Recorded frame at time {frame.time}: Shoot = {frame.isShooting}");
    }

    private void OnClimb(Vector2 direction)
    {
        lastClimbDirection = direction;

        if (!IsRecording)
            return;

        EchoFrameData frame = new EchoFrameData
        {
            time = timeManager.GameTime,
            climbDirection = direction,
        };

        echoData.frames.Add(frame);

        Debug.Log($"Recorded frame at time {frame.time}: Move Direction = {frame.moveDirection}");
    }

    private void OnActivate()
    {
        if (!IsRecording)
            return;

        EchoFrameData frame = new EchoFrameData
        {
            time = timeManager.GameTime,
            moveDirection = lastMoveDirection,
            climbDirection = lastClimbDirection,
            isActivating = true
        };
        echoData.frames.Add(frame);

        Debug.Log($"Recorded frame at time {frame.time}: Activate");
    }

    private void CheckRecording()
    {
        if (!IsRecording)
            return;

        if (timeManager.GameTime - echoData.recordStartTime >= recordingDuration)
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
        if(echoData.recordStopTime < timeManager.GameTime - offset)
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

            OnPlayingStopped?.Invoke();
        }
        else
        {
            for (int i = 0; i < echoData.frames.Count; i++)
            {
                if (echoData.frames[0].time < timeManager.GameTime - offset)
                {
                    ghost.SetEchoFrameData(echoData.frames[0]);
                    echoData.frames.RemoveAt(0);
                    i--;
                }
            }
        }
    }
}
