using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class JumpGhost : MonoBehaviour, IJump, IOnJump
    {
        [SerializeField] private float jumpForce = 5f;
        [Space]
        [SerializeField] private LayerMask groundLayer = default;
        [SerializeField] private Transform groundCheck = null;
        [SerializeField] private float groundCheckRadius = 0.2f;

        private Rigidbody2D _rigidbody2D = null;
        private Ghost _ghost = null;

        public Action OnJump { get; set; }

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _ghost = GetComponent<Ghost>();
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
            if (_ghost?.currentFrame == null || !_ghost.currentFrame.isJumping)
                return;

            if (IsGrounded())
            {
                _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, 0f);
                _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

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