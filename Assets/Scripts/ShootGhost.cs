using UnityEngine;

public class ShootGhost : MonoBehaviour
{
    private IShoot iShoot = null;
    private Ghost ghost = null;

    private void Awake()
    {
        iShoot = GetComponent<IShoot>();
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
        Shoot();
    }

    private void Shoot()
    {
        if (ghost?.currentFrame == null || !ghost.currentFrame.isShooting)
            return;

        iShoot?.Shoot();
    }
}
