using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveInput : MonoBehaviour
{
    private IMove iMove = null;
    private PlayerInput playerInput = null;
    private InputAction moveAction = null;

    public Action<Vector2> OnMove { get; set; }

    private void Awake()
    {
        iMove = GetComponent<IMove>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
    }

    private void OnEnable()
    {
        moveAction.performed += ctx => OnMoveAction(ctx.ReadValue<Vector2>());
        moveAction.canceled += ctx => OnMoveAction(Vector2.zero);
    }

    private void OnDisable()
    {
        moveAction.performed -= ctx => OnMoveAction(ctx.ReadValue<Vector2>());
        moveAction.canceled -= ctx => OnMoveAction(Vector2.zero);
    }

    private void OnMoveAction(Vector2 direction)
    {
        iMove?.Move(direction);
    }
}