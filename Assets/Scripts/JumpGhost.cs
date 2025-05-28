using System;
using UnityEngine;

namespace Game
{
    public class JumpGhost : MonoBehaviour, IJump, IOnJump
    {
        [SerializeField] private float jumpForce = 5f;
        [Space]
        [SerializeField] private LayerMask groundLayer = default;
        [SerializeField] private Transform groundCheck = null;
        [SerializeField] private float groundCheckRadius = 0.2f;

        private Rigidbody2D rigidbody = null;
        private Ghost ghost = null;

        public Action OnJump { get; set; }

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            ghost = GetComponent<Ghost>();
        }

        private void OnDrawGizmos()
        {
            if (groundCheck != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            }
        }

        public void Jump()
        {
            if (ghost?.currentFrame == null || !ghost.currentFrame.isJumping)
                return;

            if (IsGrounded())
            {
                rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, 0f);
                rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                OnJump?.Invoke();
            }
        }

        private bool IsGrounded()
        {
            if (groundCheck == null)
                return false;

            return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }
    }
}