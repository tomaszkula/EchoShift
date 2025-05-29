using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ShootInput : MonoBehaviour
{
    private bool isStartShootingRequested = false;
    private bool isShooting = false;
    private bool isCancelShootingRequested = false;

    private IShoot iShoot = null;
    private PlayerInput playerInput = null;
    private InputAction shootAction = null;

    private void Awake()
    {
        iShoot = GetComponent<IShoot>();
        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions["Attack"];
    }

    private void OnEnable()
    {
        shootAction.started += ctx => OnShootActionStarted();
        shootAction.canceled += ctx => OnShootActionCanceled();
    }

    private void Update()
    {
        StartShooting();
        PerformShooting();
        CancelShooting();
    }

    private void OnDisable()
    {
        shootAction.started -= ctx => OnShootActionStarted();
        shootAction.canceled -= ctx => OnShootActionCanceled();
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
