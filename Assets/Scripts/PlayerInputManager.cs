using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : BaseManager
{
    [Header("References")]
    [SerializeField] private PlayerInput playerInput = null;

    private InputAction moveAction = null;
    private InputAction jumpAction = null;
    private InputAction shootAction = null;
    private InputAction activateAction = null;

    public event Action<Vector2> OnMove = null;
    public event Action OnJump = null;
    public event Action OnShootStart = null;
    public event Action OnShootEnd = null;
    public event Action OnActivate = null;

    public override void Initialize()
    {
        moveAction  = playerInput.actions["Move"];
        jumpAction  = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Attack"];
        activateAction = playerInput.actions["Interact"];

        moveAction.performed += ctx => OnMove?.Invoke(ctx.ReadValue<Vector2>());
        moveAction.canceled += ctx => OnMove?.Invoke(Vector2.zero);
        jumpAction.performed += ctx => OnJump?.Invoke();
        shootAction.started += ctx => OnShootStart?.Invoke();
        shootAction.canceled += ctx => OnShootEnd?.Invoke();
        activateAction.performed += ctx => OnActivate?.Invoke();

        StartCoroutine(InitializeCoroutine());
    }

    public override void Deinitialize()
    {
        moveAction.performed -= ctx => OnMove?.Invoke(ctx.ReadValue<Vector2>());
        moveAction.canceled -= ctx => OnMove?.Invoke(Vector2.zero);
        jumpAction.performed -= ctx => OnJump?.Invoke();
        shootAction.started -= ctx => OnShootStart?.Invoke();
        shootAction.canceled -= ctx => OnShootEnd?.Invoke();
        activateAction.performed -= ctx => OnActivate?.Invoke();

        if(ManagersController.Instance != null &&
           ManagersController.Instance.GetManager<PauseManager>().isInitialized)
        {
            ManagersController.Instance.GetManager<PauseManager>().OnPause -= () => playerInput.DeactivateInput();
            ManagersController.Instance.GetManager<PauseManager>().OnResume -= () => playerInput.ActivateInput();
        }

        base.Deinitialize();
    }

    private IEnumerator InitializeCoroutine()
    {
        yield return new WaitUntil(() => ManagersController.Instance.GetManager<PauseManager>().isInitialized);

        ManagersController.Instance.GetManager<PauseManager>().OnPause += () => playerInput.DeactivateInput();
        ManagersController.Instance.GetManager<PauseManager>().OnResume += () => playerInput.ActivateInput();

        base.Initialize();
    }
}
