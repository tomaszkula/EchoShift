using UnityEngine;

namespace Game
{
    public class CharacterController : MonoBehaviour
    {
        public IMove move { get; private set; }
        public IOnMove onMove { get; private set; } 

        public IJump jump { get; private set; }
        public IOnJump onJump { get; private set; }

        public IShoot shoot { get; private set; }
        public IOnShoot onShoot { get; private set; }

        private void Awake()
        {
            move = GetComponent<IMove>();
            onMove = GetComponent<IOnMove>();

            jump = GetComponent<IJump>();
            onJump = GetComponent<IOnJump>();

            shoot = GetComponent<IShoot>();
            onShoot = GetComponent<IOnShoot>();
        }

        private void Update()
        {
            move?.Move();
            jump?.Jump();
        }
    }
}