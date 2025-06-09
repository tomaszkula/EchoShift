using System;
using UnityEngine;

public class JumpDefault : MonoBehaviour, IJump
{
    [Header("Temporary Settings")]
    [SerializeField] private bool skipGroundCheck = false; // TODO: improve ghost frame shooting logic

    [Header("Settings")]
    [SerializeField] private float jumpForce = 5f;

    private IFeet iFeet = null;
    private Rigidbody2D rigidbody = null;

    public event Action OnJump = null;

    private void Awake()
    {
        iFeet = GetComponent<IFeet>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        if (!skipGroundCheck && !iFeet.IsGrounded())
            return;

        rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, 0f);
        rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        OnJump?.Invoke();
    }
}
