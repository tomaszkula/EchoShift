using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class MoveInput : MonoBehaviour, IMove, IOnMove
    {
        [SerializeField] private float speed = 5f;

        private Vector2 moveDirection;

        private Rigidbody2D _rigidbody2D = null;
        private PlayerInput _playerInput = null;
        private InputAction _moveAction = null;

        public Action<Vector2> OnMove { get; set; }

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _playerInput = GetComponent<PlayerInput>();
            _moveAction = _playerInput.actions["Move"];
        }

        private void OnEnable()
        {
            _moveAction.performed += ctx => OnMoveAction(ctx.ReadValue<Vector2>());
            _moveAction.canceled += ctx => OnMoveAction(Vector2.zero);
        }

        private void OnDisable()
        {
            _moveAction.performed -= ctx => OnMoveAction(ctx.ReadValue<Vector2>());
            _moveAction.canceled -= ctx => OnMoveAction(Vector2.zero);
        }

        private void OnMoveAction(Vector2 direction)
        {
            moveDirection = direction.normalized;
        }

        public void Move()
        {
            Vector2 velocity = moveDirection * speed;
            velocity.y = _rigidbody2D.linearVelocity.y;

            _rigidbody2D.linearVelocity = velocity;

            OnMove?.Invoke(moveDirection);
        }
    }
}