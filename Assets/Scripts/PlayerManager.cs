using UnityEngine;

namespace Game
{
    public class PlayerManager : BaseManager
    {
        [SerializeField] private CharacterController playerPrefab = null;

        public CharacterController player { get; private set; }

        private PlayerSpawner _playerSpawner = null;

        private void Awake()
        {
            _playerSpawner = FindAnyObjectByType<PlayerSpawner>();
        }

        public override void Initialize()
        {
            Vector3 spawnPosition = Vector3.zero;
            if(_playerSpawner != null)
            {
                spawnPosition = _playerSpawner.spawnPosition;
            }
            player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);

            GameManager.Instance.GetManager<GhostsManager>().Setup(player.onMove, player.onJump, player.onShoot);

            base.Initialize();
        }
    }
}