using System;
using UnityEngine;

public class MoveGhost : MonoBehaviour
{
    private IMove iMove = null;
    private Ghost ghost = null;

    public Action<Vector2> OnMove { get; set; }

    private void Awake()
    {
        iMove = GetComponent<IMove>();
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
        Move();
    }

    public void Move()
    {
        if (ghost?.currentFrame == null)
            return;

        iMove?.Move(ghost.currentFrame.moveDirection);
    }
}