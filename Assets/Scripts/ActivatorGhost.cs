using UnityEngine;

public class ActivatorGhost : MonoBehaviour
{
    private IActivator iActivator = null;
    private Ghost ghost = null;

    private void Awake()
    {
        iActivator = GetComponent<IActivator>();
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
        Activate();
    }

    private void Activate()
    {
        if (ghost?.currentFrame == null || !ghost.currentFrame.isActivating)
            return;

        iActivator?.Activate();
    }
}
