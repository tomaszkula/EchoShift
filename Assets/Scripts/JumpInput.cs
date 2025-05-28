using UnityEngine;
using UnityEngine.InputSystem;

public class JumpInput : MonoBehaviour
{
    private IJump iJump = null;
    private PlayerInput playerInput = null;
    private InputAction jumpAction = null;

    private void Awake()
    {
        iJump = GetComponent<IJump>();
        playerInput = GetComponent<PlayerInput>();
        jumpAction = playerInput.actions["Jump"];
    }

    private void OnEnable()
    {
        jumpAction.performed += ctx => OnJumpAction();
    }

    private void OnDisable()
    {
        jumpAction.performed -= ctx => OnJumpAction();
    }

    private void OnJumpAction()
    {
        iJump?.Jump();
    }
}