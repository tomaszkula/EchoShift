using System.Collections;
using UnityEngine;

public class PlayerManager : BaseManager
{
    public Game.CharacterController player { get; private set; }

    private PlayerSpawner playerSpawner = null;

    private void Awake()
    {
        playerSpawner = FindAnyObjectByType<PlayerSpawner>();
    }

    //public override void Initialize()
    //{
    //    IEnumerator InitializeCoroutine()
    //    {
    //        yield return new WaitUntil(() => GameManager.Instance.GetManager<ObjectPoolsManager>().IsInitialized);

    //        Vector3 spawnPosition = Vector3.zero;
    //        if (playerSpawner != null)
    //        {
    //            spawnPosition = playerSpawner.spawnPosition;
    //        }

    //        GameObject playerGo = GameManager.Instance.GetManager<ObjectPoolsManager>().GetPool(ObjectPoolsManager.PoolType.Player).Get();
    //        player = playerGo.GetComponent<Game.CharacterController>();
    //        player.transform.position = spawnPosition;
    //        player.transform.rotation = Quaternion.identity;
    //        playerGo.transform.SetParent(null);

    //        GameManager.Instance.GetManager<GhostsManager>().Setup(player.iMove, player.iJump, player.iShoot, player.iActivator);

    //        base.Initialize();
    //    }

    //    StartCoroutine(InitializeCoroutine());
    //}

    //public override void Deinitialize()
    //{
    //    if(GameManager.IsInitialized &&
    //        GameManager.Instance.GetManager<ObjectPoolsManager>().IsInitialized)
    //    {
    //        GameManager.Instance.GetManager<ObjectPoolsManager>().GetPool(ObjectPoolsManager.PoolType.Player).Release(player.gameObject);
    //    }

    //    base.Deinitialize();
    //}
}