using System;
using UnityEngine;

public class ClimbGhost : MonoBehaviour
{
    private IClimb iClimb = null;
    private Ghost ghost = null;

    public Action<Vector2> OnClimb { get; set; }

    private void Awake()
    {
        iClimb = GetComponent<IClimb>();
        ghost = GetComponent<Ghost>();
    }

    private void OnEnable()
    {
        if (ghost != null)
            ghost.onFrameUpdated += OnFrameUpdated;
    }

    private void OnDisable()
    {
        if (ghost != null)
            ghost.onFrameUpdated -= OnFrameUpdated;
    }

    private void OnFrameUpdated(EchoFrameData frameData)
    {
        Climb();
    }

    public void Climb()
    {
        if (ghost?.currentFrame == null)
            return;

        iClimb?.Climb(ghost.currentFrame.climbDirection);
    }
}
