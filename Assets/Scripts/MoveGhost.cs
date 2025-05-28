using System;
using UnityEngine;

namespace Game
{
    public class MoveGhost : MonoBehaviour, IMove, IOnMove
    {
        [SerializeField] private float speed = 5f;

        private Rigidbody2D rigidbody = null;
        private Ghost ghost = null;

        public Action<Vector2> OnMove { get; set; }

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            ghost = GetComponent<Ghost>();
        }

        public void Move()
        {
            if (ghost?.currentFrame == null)
                return;

            Vector2 velocity = ghost.currentFrame.moveDirection * speed;
            velocity.y = rigidbody.linearVelocity.y;

            rigidbody.linearVelocity = velocity;

            OnMove?.Invoke(ghost.currentFrame.moveDirection);
        }
    }
}