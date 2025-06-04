using UnityEngine;

public class PlayerManager : BaseGameManager
{
    public Game.CharacterController player { get; private set; }

    protected override void DeinitializeInternal()
    {
        if (player != null
            && GameManager.IsInitialized
            && GameManager.Instance.GetManager<ObjectPoolsManager>().IsInitialized)
        {
            GameManager.Instance.GetManager<ObjectPoolsManager>().GetPool(ObjectPoolsManager.PoolType.Player).Release(player.gameObject);
        }

        base.DeinitializeInternal();
    }

    public void Spawn()
    {
        PlayerSpawner playerSpawner = FindAnyObjectByType<PlayerSpawner>();

        GameObject playerGo = GameManager.Instance.GetManager<ObjectPoolsManager>().GetPool(ObjectPoolsManager.PoolType.Player).Get();
        player = playerGo.GetComponent<Game.CharacterController>();
        player.transform.position = playerSpawner.spawnPosition;
        player.transform.rotation = Quaternion.identity;
        playerGo.transform.SetParent(null);

        GameManager.Instance.GetManager<GhostsManager>().Setup(player.iMove, player.iJump, player.iShoot, player.iActivator);
    }
}