using System;
using UnityEngine;

public class JumpGhost : MonoBehaviour
{
    private IJump iJump = null;
    private Ghost ghost = null;

    private void Awake()
    {
        iJump = GetComponent<IJump>();
        ghost = GetComponent<Ghost>();
    }

    private void OnEnable()
    {
        if (ghost != null)
        {
            ghost.onFrameUpdated += OnFrameUpdated;
        }
    }

    private void OnDisable()
    {
        if (ghost != null)
        {
            ghost.onFrameUpdated -= OnFrameUpdated;
        }
    }

    private void OnFrameUpdated(EchoFrameData frameData)
    {
        Jump();
    }

    private void Jump()
    {
        if (ghost?.currentFrame == null || !ghost.currentFrame.isJumping)
            return;

        iJump?.Jump();
    }
}