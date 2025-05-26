using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class JumpInput : MonoBehaviour, IJump
    {
        [SerializeField] private float jumpForce = 5f; // You can expose this as a public field if needed

        private InputAction _jumpAction = null;
        private bool _jumpRequested = false;

        private Rigidbody2D _rb = null;
        private PlayerInput _playerInput = null;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _playerInput = GetComponent<PlayerInput>();
        }

        private void OnEnable()
        {
            if (_playerInput != null)
            {
                _jumpAction = _playerInput.actions["Jump"];

                if (_jumpAction != null)
                {
                    _jumpAction.performed += ctx => OnJump();
                }
            }
        }

        private void OnDisable()
        {
            if (_jumpAction != null)
            {
                _jumpAction.performed -= ctx => OnJump();
            }
        }

        private void OnJump()
        {
            _jumpRequested = true;
        }

        public LayerMask groundLayer; // Expose in Inspector to set ground layer
        [SerializeField] private Transform groundCheck; // Assign a child transform at feet position
        [SerializeField] private float groundCheckRadius = 0.2f;

        private bool IsGrounded()
        {
            if (groundCheck == null) return false;
            return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }

        public void Jump()
        {
            if (_jumpRequested)
            {
                _jumpRequested = false;

                if (_rb != null && IsGrounded())
                {
                    _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);
                    _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}