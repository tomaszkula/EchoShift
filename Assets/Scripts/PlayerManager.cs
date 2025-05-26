using UnityEngine;

namespace Game
{
    public class PlayerManager : BaseManager
    {
        [SerializeField] private CharacterController playerPrefab = null;

        public CharacterController player { get; private set; }

        public override void Initialize()
        {
            base.Initialize();

            player = Instantiate(playerPrefab);

            GameManager.Instance.GetManager<GhostsManager>().Setup(player.onMove, player.onJump);
        }
    }
}