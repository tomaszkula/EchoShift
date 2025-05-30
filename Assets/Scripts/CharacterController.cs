using UnityEngine;

namespace Game
{
    public class CharacterController : MonoBehaviour
    {
        public IMove iMove { get; private set; }
        public IJump iJump { get; private set; }
        public IShoot iShoot { get; private set; }

        public IFace iFace { get; private set; }

        private void Awake()
        {
            iMove = GetComponent<IMove>();
            iJump = GetComponent<IJump>();
            iShoot = GetComponent<IShoot>();
            
            iFace = GetComponent<IFace>();
        }
    }
}