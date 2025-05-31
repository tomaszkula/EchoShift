using System;
using UnityEngine;

public class MoveDefault : MonoBehaviour, IMove
{
    [Header("Settings")]
    [SerializeField] private float speed = 5f;

    private Vector2 moveDirection = Vector2.zero;

    private Rigidbody2D rigidbody = null;

    public event Action<Vector2> OnMove = null;

    private void Awake()
    {
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
        Vector2 velocity = moveDirection * speed;
        velocity.y = rigidbody.linearVelocity.y;

        rigidbody.linearVelocity = velocity;

        OnMove?.Invoke(moveDirection);
    }
}
