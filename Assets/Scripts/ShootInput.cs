using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ShootInput : MonoBehaviour
{
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
        shootAction.performed += ctx => OnShootAction();
    }

    private void OnDisable()
    {
        shootAction.performed -= ctx => OnShootAction();
    }

    private void OnShootAction()
    {
        if(EventSystem.current.IsPointerOverGameObject())
            return;

        iShoot?.Shoot();
    }
}
