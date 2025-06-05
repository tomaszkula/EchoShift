using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputsManager : BaseManager
{
    [Header("References")]
    [SerializeField] private PlayerInput playerInput = null;

    private InputAction moveIA = null;
    private InputAction jumpIA = null;
    private InputAction shootIA = null;
    private InputAction activateIA = null;

    public event Action<Vector2> OnMove = null;
    public event Action OnJump = null;
    public event Action OnShootStart = null;
    public event Action OnShootEnd = null;
    public event Action OnActivate = null;

    private void Awake()
    {
        moveIA = playerInput.actions["Move"];
        jumpIA = playerInput.actions["Jump"];
        shootIA = playerInput.actions["Attack"];
        activateIA = playerInput.actions["Interact"];
    }

    protected override async void InitializeInternal()
    {
        base.InitializeInternal();

        moveIA.performed += ctx => OnMove?.Invoke(ctx.ReadValue<Vector2>());
        moveIA.canceled += ctx => OnMove?.Invoke(Vector2.zero);
        jumpIA.performed += ctx => OnJump?.Invoke();
        shootIA.started += ctx => OnShootStart?.Invoke();
        shootIA.canceled += ctx => OnShootEnd?.Invoke();
        activateIA.performed += ctx => OnActivate?.Invoke();

        await new WaitUntil(() => Manager.Instance.GetManager<PauseManager>().IsInitialized);

        Manager.Instance.GetManager<PauseManager>().OnPaused += OnPaused;
        Manager.Instance.GetManager<PauseManager>().OnResumed += OnResumed;
    }

    protected override void DeinitializeInternal()
    {
        base.DeinitializeInternal();

        moveIA.performed -= ctx => OnMove?.Invoke(ctx.ReadValue<Vector2>());
        moveIA.canceled -= ctx => OnMove?.Invoke(Vector2.zero);
        jumpIA.performed -= ctx => OnJump?.Invoke();
        shootIA.started -= ctx => OnShootStart?.Invoke();
        shootIA.canceled -= ctx => OnShootEnd?.Invoke();
        activateIA.performed -= ctx => OnActivate?.Invoke();

        OnMove = null;
        OnJump = null;
        OnShootStart = null;
        OnShootEnd = null;
        OnActivate = null;

        if (Manager.IsInitialized &&
            Manager.Instance.GetManager<PauseManager>().IsInitialized)
        {
            Manager.Instance.GetManager<PauseManager>().OnPaused -= OnPaused;
            Manager.Instance.GetManager<PauseManager>().OnResumed -= OnResumed;
        }
    }

    private void OnPaused()
    {
        playerInput.DeactivateInput();
    }

    private void OnResumed()
    {
        playerInput.ActivateInput();
    }
}
