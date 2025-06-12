using System;
using UnityEngine;

public class MoveDefault : MonoBehaviour, IMove
{
    [Header("Settings")]
    [SerializeField] private float speed = 5f;

    private Vector2 moveDirection = Vector2.zero;

    private IClimb iClimb = null;
    new private Rigidbody2D rigidbody = null;

    public event Action<Vector2> OnMove = null;

    private void Awake()
    {
        iClimb = GetComponent<IClimb>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    public void Move(Vector2 direction)
    {
        moveDirection = direction;
    }

    private void ApplyMovement()
    {
        if (iClimb.IsClimbing)
            return;

        Vector2 moveVelocity = moveDirection * speed;
        moveVelocity.y = rigidbody.linearVelocity.y;
        rigidbody.linearVelocity = moveVelocity;

        OnMove?.Invoke(moveDirection);
    }
}
