using UnityEngine;

public class PlayerManager : BaseManager
{
    public Vector2 SpawnPosition { get; set; } = Vector2.zero;

    public Character Player { get; private set; } = null;

    protected override void DeinitializeInternal()
    {
        base.DeinitializeInternal();

        if (Player != null &&
            Manager.IsInitialized &&
            Manager.Instance.GetManager<ObjectsPoolsManager>().IsInitialized)
        {
            if (Player.TryGetComponent(out IPooledObject pooledObject))
            {
                pooledObject.Pool.Release(Player.gameObject);
            }
            else
            {
                Destroy(Player.gameObject);
            }
        }
    }

    public void Spawn()
    {
        ObjectsPoolType playerOPT = Manager.Instance.GetManager<ObjectsPoolsManager>().PlayerOPT;
        GameObject playerGo = Manager.Instance.GetManager<ObjectsPoolsManager>().GetPool(playerOPT).Get();
        Player = playerGo.GetComponent<Character>();
        playerGo.transform.SetParent(null);
        playerGo.transform.SetPositionAndRotation(SpawnPosition, Quaternion.identity);

        Manager.Instance.GetManager<GhostsManager>().Setup(Player.iMove, Player.iJump, Player.iShoot, Player.iActivator);
    }
}