using UnityEngine;

namespace Game
{
    public class CharacterController : MonoBehaviour
    {
        public IMove move { get; private set; }
        public IOnMove onMove { get; private set; } 

        public IJump jump { get; private set; }
        public IOnJump onJump { get; private set; }

        private void Awake()
        {
            move = GetComponent<IMove>();
            onMove = GetComponent<IOnMove>();

            jump = GetComponent<IJump>();
            onJump = GetComponent<IOnJump>();
        }

        private void Update()
        {
            move?.Move();
            jump?.Jump();
        }
    }
}