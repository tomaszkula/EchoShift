using UnityEngine;
using UnityEngine.InputSystem;

public class ActivatorInput : MonoBehaviour
{
    private IActivator iActivator = null;
    private PlayerInput playerInput = null;
    private InputAction activateAction = null;

    private void Awake()
    {
        iActivator = GetComponent<IActivator>();
        playerInput = GetComponent<PlayerInput>();
        activateAction = playerInput.actions["Interact"];
    }

    private void OnEnable()
    {
        activateAction.performed += ctx => OnActivateAction();
    }

    private void OnDisable()
    {
        activateAction.performed -= ctx => OnActivateAction();
    }

    private void OnActivateAction()
    {
        iActivator?.Activate();
    }
}
