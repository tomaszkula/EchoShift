using UnityEngine;

public class PlayerManager : BaseManager
{
    public Vector2 SpawnPosition { get; set; } = Vector2.zero;

    public Character player { get; private set; } = null;

    protected override void DeinitializeInternal()
    {
        base.DeinitializeInternal();

        if (player != null &&
            Manager.IsInitialized &&
            Manager.Instance.GetManager<ObjectsPoolsManager>().IsInitialized)
        {
            if (player.TryGetComponent(out IPooledObject pooledObject))
            {
                pooledObject.Pool.Release(player.gameObject);
            }
            else
            {
                Destroy(player.gameObject);
            }
        }
    }

    public void Spawn()
    {
        ObjectsPoolType playerOPT = Manager.Instance.GetManager<ObjectsPoolsManager>().PlayerOPT;
        GameObject playerGo = Manager.Instance.GetManager<ObjectsPoolsManager>().GetPool(playerOPT).Get();
        player = playerGo.GetComponent<Character>();
        playerGo.transform.SetParent(null);
        playerGo.transform.SetPositionAndRotation(SpawnPosition, Quaternion.identity);

        GameManager.Instance.GetManager<GhostsManager>().Setup(player.iMove, player.iJump, player.iShoot, player.iActivator);
    }
}