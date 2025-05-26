using Game;
using System.Collections.Generic;
using UnityEngine;

public class GhostsManager : BaseManager
{
    [SerializeField] private Ghost ghostPrefab = null;

    private bool isRecording = false;
    private EchoData echoData = new EchoData();

    private IOnMove _onMove = null;
    private IOnJump _onJump = null;

    public void Setup(IOnMove onMove, IOnJump onJump)
    {
        if (_onMove != null)
        {
            _onMove.OnMove -= OnMove;
        }

        if (_onJump != null)
        {
            _onJump.OnJump -= OnJump;
        }

        _onMove = onMove;
        _onJump = onJump;

        if (_onMove != null)
        {
            _onMove.OnMove += OnMove;
        }

        if (_onJump != null)
        {
            _onJump.OnJump += OnJump;
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

        echoData = new EchoData();
        echoData.recordPosition = GameManager.Instance.GetManager<PlayerManager>().player.transform.position;
        echoData.recordTime = GameManager.Instance.GameTime;
        echoData.frames = new List<EchoFrameData>();
    }

    public void StopRecording()
    {
        if (!isRecording)
        {
            Debug.LogWarning("Not currently recording. Please start a recording first.");
            return;
        }

        isRecording = false;
    }

    public void PlayRecording()
    {
        if (!isRecording)
        {
            return;
        }

        isRecording = false;

        echoData.playTime = GameManager.Instance.GameTime;

        Ghost ghost = Instantiate(ghostPrefab, echoData.recordPosition, Quaternion.identity);
        ghost.Play(echoData);
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
}
