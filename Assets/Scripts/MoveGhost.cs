using System;
using UnityEngine;

namespace Game
{
    public class MoveGhost : MonoBehaviour, IMove, IOnMove
    {
        [SerializeField] private float speed = 5f;

        private Rigidbody2D _rigidbody2D = null;
        private Ghost _ghost = null;

        public Action<Vector2> OnMove { get; set; }

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _ghost = GetComponent<Ghost>();
        }

        public void Move()
        {
            if (_ghost?.currentFrame == null)
                return;

            Vector2 velocity = _ghost.currentFrame.moveDirection * speed;
            velocity.y = _rigidbody2D.linearVelocity.y;

            _rigidbody2D.linearVelocity = velocity;

            OnMove?.Invoke(_ghost.currentFrame.moveDirection);
        }
    }
}