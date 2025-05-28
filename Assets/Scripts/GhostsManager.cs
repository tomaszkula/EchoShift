using Game;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GhostsManager : BaseManager
{
    [Header("Settings")]
    [SerializeField] private float recordingDuration = 5f;
    [SerializeField] private Ghost ghostPrefab = null;

    private EchoData echoData = new EchoData();
    public bool isRecording { get; private set; } = false;
    public bool isRecorded {  get; private set; } = false;
    public bool isPlaying { get; private set; } = false;

    private Ghost _ghost = null;

    private IOnMove onMove = null;
    private IOnJump onJump = null;
    private IOnShoot onShoot = null;

    public event Action<float, float> onRecordingStarted = null;
    public event Action onRecordingStopped = null;
    public event Action<float, float> onPlayingStarted = null;
    public event Action onPlayingStopped = null;

    private void Update()
    {
        CheckRecording();
        Play();
    }

    public void Setup(IOnMove onMove, IOnJump onJump, IOnShoot onShoot)
    {
        if (this.onMove != null)
        {
            this.onMove.OnMove -= OnMove;
        }

        if (this.onJump != null)
        {
            this.onJump.OnJump -= OnJump;
        }

        if (this.onShoot != null)
        {
            this.onShoot.OnShoot -= OnShoot;
        }

        this.onMove = onMove;
        this.onJump = onJump;
        this.onShoot = onShoot;

        if (this.onMove != null)
        {
            this.onMove.OnMove += OnMove;
        }

        if (this.onJump != null)
        {
            this.onJump.OnJump += OnJump;
        }

        if (this.onShoot != null)
        {
            this.onShoot.OnShoot += OnShoot;
        }
    }

    public void StartRecording()
    {
        if (isRecording)
        {
            Debug.LogWarning("Already recording. Please stop the current recording before starting a new one.");
            return;
        }

        isRecording = true;
        isRecorded = false;

        echoData = new EchoData();
        echoData.recordPosition = GameManager.Instance.GetManager<PlayerManager>().player.transform.position;
        echoData.recordStartTime = GameManager.Instance.GameTime;
        echoData.frames = new List<EchoFrameData>();

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

        _ghost = Instantiate(ghostPrefab, echoData.recordPosition, Quaternion.identity);

        onPlayingStarted?.Invoke(recordingDuration, echoData.playStartTime);
    }

    private void OnMove(Vector2 moveDirection)
    {
        if (!isRecording)
            return;

        EchoFrameData frame = new EchoFrameData
        {
            time = GameManager.Instance.GameTime,
            moveDirection = moveDirection
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
            isShooting = true
        };

        echoData.frames.Add(frame);

        Debug.Log($"Recorded frame at time {frame.time}: Shoot = {frame.isShooting}");
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

        if (echoData?.frames?.Count < 1)
        {
            return;
        }

        float offset = echoData.playStartTime - echoData.recordStartTime;
        if(echoData.recordStopTime < GameManager.Instance.GameTime - offset)
        {
            isPlaying = false;
            Destroy(_ghost.gameObject);
            _ghost = null;

            onPlayingStopped?.Invoke();
        }
        else if (echoData.frames[0].time < GameManager.Instance.GameTime - offset)
        {
            _ghost.SetEchoFrameData(echoData.frames[0]);
            echoData.frames.RemoveAt(0);
        }
        //else
        //{
        //    _ghost.SetEchoFrameData(null);
        //}
    }
}
