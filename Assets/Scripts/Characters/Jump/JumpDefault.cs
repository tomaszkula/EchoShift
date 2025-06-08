using Game;
using System;
using UnityEngine;

public class JumpDefault : MonoBehaviour, IJump
{
    [Header("Temporary Settings")]
    [SerializeField] private bool skipGroundCheck = false; // TODO: improve ghost frame shooting logic

    [Header("Settings")]
    [SerializeField] private float jumpForce = 5f;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer = default;
    [SerializeField] private Transform groundCheck = null;
    [SerializeField] private float groundCheckRadius = 0.2f;

    private Rigidbody2D rigidbody = null;

    public event Action OnJump = null;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    public void Jump()
    {
        if (!IsGrounded())
            return;

        rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, 0f);
        rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        OnJump?.Invoke();
    }

    private bool IsGrounded()
    {
        if (skipGroundCheck)
            return true;

        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
}
