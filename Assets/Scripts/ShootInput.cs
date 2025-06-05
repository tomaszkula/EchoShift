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
        Manager.Instance.GetManager<InputsManager>().OnShootStart += OnShootActionStarted;
        Manager.Instance.GetManager<InputsManager>().OnShootEnd += OnShootActionCanceled;
    }

    private void Update()
    {
        StartShooting();
        PerformShooting();
        CancelShooting();
    }

    private void OnDisable()
    {
        Manager.Instance.GetManager<InputsManager>().OnShootStart -= OnShootActionStarted;
        Manager.Instance.GetManager<InputsManager>().OnShootEnd -= OnShootActionCanceled;
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
