using System;
using UnityEngine;

public class ShootDefault : MonoBehaviour, IShoot
{
    [Header("Temporary Settings")]
    [SerializeField] private bool skipCooldown = false; // TODO: improve ghost frame shooting logic

    [Header("Settings")]
    [SerializeField] private WeaponData weaponData = null;

    private float shootDelay = 0f;

    private IHand iHand = null;
    private IFace iFace = null;

    public event Action OnShoot = null;

    private void Awake()
    {
        iHand = GetComponent<IHand>();
        iFace = GetComponent<IFace>();
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
        if (!skipCooldown && shootDelay > 0f)
            return;

        shootDelay = weaponData.ProjectileCooldown;

        ObjectsPoolType projectileOPT = Manager.Instance.GetManager<ObjectsPoolsManager>().ProjectileOPT;
        GameObject projectileGo = Manager.Instance.GetManager<ObjectsPoolsManager>().GetPool(projectileOPT).Get();
        if (projectileGo.TryGetComponent(out Projectile projectile))
        {
            projectile.transform.position = iHand.Hand.position;
            projectile.transform.rotation = iFace.FaceDirection switch
            {
                Direction.Right => Quaternion.Euler(0, 0, 0),
                Direction.Left => Quaternion.Euler(0, 180, 0),
                _ => Quaternion.identity
            };
            projectile.GetComponent<IAttacker>().Attacker = gameObject;
        }

        OnShoot?.Invoke();
    }
}
