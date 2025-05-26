using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class JumpInput : MonoBehaviour, IJump
    {
        [SerializeField] private float jumpForce = 5f; // You can expose this as a public field if needed
        [Space]
        [SerializeField] private LayerMask groundLayer; // Expose in Inspector to set ground layer
        [SerializeField] private Transform groundCheck; // Assign a child transform at feet position
        [SerializeField] private float groundCheckRadius = 0.2f;

        private bool _jumpRequested = false;

        private Rigidbody2D _rb = null;
        private PlayerInput _playerInput = null;
        private InputAction _jumpAction = null;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _playerInput = GetComponent<PlayerInput>();
            _jumpAction = _playerInput.actions["Jump"];
        }

        private void OnEnable()
        {
            _jumpAction.performed += ctx => OnJump();
        }

        private void OnDisable()
        {
            _jumpAction.performed -= ctx => OnJump();
        }

        private void OnJump()
        {
            _jumpRequested = true;
        }

        public void Jump()
        {
            if (!_jumpRequested)
                return;

            _jumpRequested = false;

            if (IsGrounded())
            {
                //_rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);
                _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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