using System;
using UnityEngine;

public class MoveDefault : MonoBehaviour, IMove
{
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rigidbody = null;

    public event Action<Vector2> OnMove = null;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        Vector2 velocity = direction * speed;
        velocity.y = rigidbody.linearVelocity.y;

        rigidbody.linearVelocity = velocity;

        OnMove?.Invoke(direction);
    }
}
