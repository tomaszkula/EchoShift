using UnityEngine;
using UnityEngine.EventSystems;

public class ShootInput : MonoBehaviour
{
    private bool isStartShootingRequested = false;
    private bool isShooting = false;
    private bool isCancelShootingRequested = false;

    private IShoot iShoot = null;

    private void Awake()
    {
        iShoot = GetComponent<IShoot>();
    }

    private void OnEnable()
    {
        ManagersController.Instance.GetManager<PlayerInputManager>().OnShootStart += OnShootActionStarted;
        ManagersController.Instance.GetManager<PlayerInputManager>().OnShootEnd += OnShootActionCanceled;
    }

    private void Update()
    {
        StartShooting();
        PerformShooting();
        CancelShooting();
    }

    private void OnDisable()
    {
        ManagersController.Instance.GetManager<PlayerInputManager>().OnShootStart -= OnShootActionStarted;
        ManagersController.Instance.GetManager<PlayerInputManager>().OnShootEnd -= OnShootActionCanceled;
    }

    private void OnShootActionStarted()
    {
        isStartShootingRequested = true;
    }

    private void OnShootActionCanceled()
    {
        isCancelShootingRequested = true;
    }

    private void StartShooting()
    {
        if (!isStartShootingRequested)
            return;

        isStartShootingRequested = false;

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        isShooting = true;
    }

    private void PerformShooting()
    {
        if (!isShooting)
            return;

        iShoot?.Shoot();
    }

    private void CancelShooting()
    {
        if(!isCancelShootingRequested)
            return;

        isCancelShootingRequested = false;

        if (!isShooting)
            return;

        isShooting = false;
    }
}
