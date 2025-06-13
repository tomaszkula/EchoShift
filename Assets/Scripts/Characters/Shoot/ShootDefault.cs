using System;
using UnityEngine;
using UnityEngine.Pool;

public class ShootDefault : MonoBehaviour, IShoot
{
    [Header("Temporary Settings")]
    [SerializeField] private bool skipCooldown = false; // TODO: improve ghost frame shooting logic

    private float shootDelay = 0f;

    private IWeaponKeeper iWeaponKeeper = null;

    public event Action OnShoot = null;

    private void Awake()
    {
        iWeaponKeeper = GetComponent<IWeaponKeeper>();
    }

    private void Update()
    {
        if(shootDelay > 0f)
        {
            shootDelay -= Time.deltaTime;
        }
    }

    public void Shoot()
    {
        if (iWeaponKeeper.WeaponData == null)
            return;

        if (!skipCooldown && shootDelay > 0f)
            return;

        shootDelay = iWeaponKeeper.WeaponData.ProjectileCooldown;

        ObjectsPoolType projectileOPT = iWeaponKeeper.WeaponData.ProjectileData.ObjectsPoolType;
        ObjectPool<GameObject> projectileObjectsPool = Manager.Instance.GetManager<ObjectsPoolsManager>().GetPool(projectileOPT);
        for (int i = 0; i < iWeaponKeeper.WeaponData.ProjectilesCount; i++)
        {
            GameObject projectileGo = projectileObjectsPool.Get();
            if (projectileGo.TryGetComponent(out Projectile projectile))
            {
                float zRotation = 0f;
                if (iWeaponKeeper.WeaponData.ProjectilesCount > 1)
                {
                    float t = i / (float)(iWeaponKeeper.WeaponData.ProjectilesCount - 1);
                    zRotation = Mathf.Lerp(iWeaponKeeper.WeaponData.ProjectilesSpawnRange.x, iWeaponKeeper.WeaponData.ProjectilesSpawnRange.y, t);
                }

                projectile.transform.position = iWeaponKeeper.WeaponInHand.transform.TransformPoint(iWeaponKeeper.WeaponData.WihMufflePosition);
                projectile.transform.rotation = Quaternion.Euler(0, 0, zRotation) * iWeaponKeeper.WeaponInHand.transform.rotation;
                projectile.GetComponent<IAttacker>().Attacker = gameObject;
            }
        }

        OnShoot?.Invoke();
    }
}
