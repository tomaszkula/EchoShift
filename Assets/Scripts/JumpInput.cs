using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class JumpInput : MonoBehaviour, IJump, IOnJump
    {
        [SerializeField] private float jumpForce = 5f;
        [Space]
        [SerializeField] private LayerMask groundLayer = default;
        [SerializeField] private Transform groundCheck = null;
        [SerializeField] private float groundCheckRadius = 0.2f;

        private bool jumpRequested = false;

        private Rigidbody2D rigidbody2D = null;
        private PlayerInput playerInput = null;
        private InputAction jumpAction = null;

        public Action OnJump { get; set; }

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
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

        private void OnDrawGizmos()
        {
            if (groundCheck != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            }
        }

        private void OnJumpAction()
        {
            jumpRequested = true;
        }

        public void Jump()
        {
            if (!jumpRequested)
                return;

            jumpRequested = false;

            if (IsGrounded())
            {
                rigidbody2D.linearVelocity = new Vector2(rigidbody2D.linearVelocity.x, 0f);
                rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

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