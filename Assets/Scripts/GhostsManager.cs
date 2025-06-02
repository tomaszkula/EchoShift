using System;
using System.Collections.Generic;
using UnityEngine;

public class GhostsManager : BaseManager
{
    [Header("Settings")]
    [SerializeField] private int _maxGhostsCount = 5;
    [SerializeField] private float recordingDuration = 5f;

    private EchoData echoData = new EchoData();
    public int maxGhostsCount => _maxGhostsCount;
    public int ghostsCount { get; private set; } = 0;
    public bool isRecording { get; private set; } = false;
    public bool isRecorded {  get; private set; } = false;
    public bool isPlaying { get; private set; } = false;
    public bool isPlayed { get; private set; } = false;

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

    //public override void Deinitialize()
    //{
    //    onRecordingStarted = null;
    //    onRecordingStopped = null;
    //    onPlayingStarted = null;
    //    onPlayingStopped = null;

    //    base.Deinitialize();
    //}

    private void Update()
    {
        CheckRecording();
        Play();
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
        if (isRecording)
        {
            Debug.LogWarning("Already recording. Please stop the current recording before starting a new one.");
            return;
        }

        if (isRecorded)
        {
            Debug.LogWarning("Already recorded. Please play the recording before starting a new one.");
            return;
        }

        if (ghostsCount >= _maxGhostsCount)
        {
            Debug.LogWarning("Maximum number of ghosts reached. Cannot start a new recording.");
            return;
        }

        ghostsCount++;
        isRecording = true;
        isPlayed = false;

        echoData = new EchoData();
        echoData.recordPosition = GameManager.Instance.GetManager<PlayerManager>().player.transform.position;
        echoData.recordFaceDirection = GameManager.Instance.GetManager<PlayerManager>().player.iFace.FaceDirection;
        echoData.recordStartTime = GameManager.Instance.GameTime;
        echoData.frames = new List<EchoFrameData>()
        {
            new EchoFrameData
            {
                time = GameManager.Instance.GameTime,
                moveDirection = lastDirection,
            }
        };

        onRecordingStarted?.Invoke(recordingDuration, echoData.recordStartTime);
    }

    public void StopRecording()
    {
        if (!isRecording)
        {
            Debug.LogWarning("Not currently recording. Please start a recording first.");
            return;
        }

        isRecording = false;
        isRecorded = true;

        echoData.recordStopTime = GameManager.Instance.GameTime;

        onRecordingStopped?.Invoke();
    }

    public void PlayRecording()
    {
        if (isRecording || !isRecorded)
        {
            return;
        }

        isRecorded = false;
        isPlaying = true;
        echoData.playStartTime = GameManager.Instance.GameTime;

        GameObject ghostGo = GameManager.Instance.GetManager<ObjectPoolsManager>().GetPool(ObjectPoolsManager.PoolType.Ghost).Get();
        ghost = ghostGo.GetComponent<Ghost>();
        ghost.transform.position = echoData.recordPosition;
        if(ghost.TryGetComponent(out IFace iFace))
        {
            iFace.FaceDirection = echoData.recordFaceDirection;
        }

        onPlayingStarted?.Invoke(recordingDuration, echoData.playStartTime);
    }

    private void OnMove(Vector2 direction)
    {
        lastDirection = direction;

        if (!isRecording)
            return;

        EchoFrameData frame = new EchoFrameData
        {
            time = GameManager.Instance.GameTime,
            moveDirection = direction
        };

        echoData.frames.Add(frame);

        Debug.Log($"Recorded frame at time {frame.time}: Move Direction = {frame.moveDirection}");
    }

    private void OnJump()
    {
        if (!isRecording)
            return;

        EchoFrameData frame = new EchoFrameData
        {
            time = GameManager.Instance.GameTime,
            moveDirection = lastDirection,
            isJumping = true
        };

        echoData.frames.Add(frame);

        Debug.Log($"Recorded frame at time {frame.time}: Jump = {frame.isJumping}");
    }

    private void OnShoot()
    {
        if (!isRecording)
            return;

        EchoFrameData frame = new EchoFrameData
        {
            time = GameManager.Instance.GameTime,
            moveDirection = lastDirection,
            isShooting = true
        };

        echoData.frames.Add(frame);

        Debug.Log($"Recorded frame at time {frame.time}: Shoot = {frame.isShooting}");
    }

    private void OnActivate()
    {
        if (!isRecording)
            return;

        EchoFrameData frame = new EchoFrameData
        {
            time = GameManager.Instance.GameTime,
            moveDirection = lastDirection,
            isActivating = true
        };
        echoData.frames.Add(frame);

        Debug.Log($"Recorded frame at time {frame.time}: Activate");
    }

    private void CheckRecording()
    {
        if (!isRecording)
            return;

        if (GameManager.Instance.GameTime - echoData.recordStartTime >= recordingDuration)
        {
            StopRecording();
            Debug.Log("Recording duration reached. Stopping recording.");
        }
    }

    private void Play()
    {
        if(!isPlaying)
        {
            return;
        }

        float offset = echoData.playStartTime - echoData.recordStartTime;
        if(echoData.recordStopTime < GameManager.Instance.GameTime - offset)
        {
            isPlaying = false;
            isPlayed = true;

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
                if (echoData.frames[0].time < GameManager.Instance.GameTime - offset)
                {
                    ghost.SetEchoFrameData(echoData.frames[0]);
                    echoData.frames.RemoveAt(0);
                    i--;
                }
            }
        }
    }
}
