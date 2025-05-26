using UnityEngine;

namespace Game
{
    public class CharacterController : MonoBehaviour
    {
        private IMove _move;
        private IJump _jump; // Assuming you have an IJump interface for jumping

        private void Awake()
        {
            _move = GetComponent<IMove>();
            _jump = GetComponent<IJump>(); // Assuming you have an IJump interface for jumping
        }

        private void Update()
        {
            _move?.Move();
            _jump?.Jump(); // Call the Jump method if the IJump interface is implemented
        }
    }
}