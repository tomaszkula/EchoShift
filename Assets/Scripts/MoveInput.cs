using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class MoveInput : MonoBehaviour, IMove
    {
        [SerializeField] private float speed = 5f;

        private Vector2 moveDirection;

        private Rigidbody2D _rigidbody2D = null;
        private PlayerInput _playerInput = null;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _playerInput = GetComponent<PlayerInput>();
        }

        private void OnEnable()
        {
            _playerInput.actions["Move"].performed += ctx => OnMove(ctx.ReadValue<Vector2>());
            _playerInput.actions["Move"].canceled += ctx => OnMove(Vector2.zero);
        }

        private void OnDisable()
        {
            _playerInput.actions["Move"].performed -= ctx => OnMove(ctx.ReadValue<Vector2>());
            _playerInput.actions["Move"].canceled -= ctx => OnMove(Vector2.zero);
        }

        private void OnMove(Vector2 direction)
        {
            moveDirection = direction.normalized;
        }

        public void Move()
        {
            if (_rigidbody2D == null)
                return;

            // Move horizontally
            Vector2 velocity = moveDirection * speed;
            velocity.y = _rigidbody2D.linearVelocity.y; // Preserve current vertical velocity (for jump)

            _rigidbody2D.linearVelocity = velocity;
        }
    }
}