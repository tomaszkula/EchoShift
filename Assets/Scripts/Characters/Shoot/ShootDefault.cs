using System;
using UnityEngine;

public class ShootDefault : MonoBehaviour, IShoot
{
    [Header("Temporary Settings")]
    [SerializeField] private bool skipCooldown = false; // TODO: improve ghost frame shooting logic

    [Header("Settings")]
    [SerializeField] private WeaponData defaultWeaponData = null;

    private WeaponData weaponData = null;
    private float shootDelay = 0f;

    private IHand iHand = null;
    private IFace iFace = null;
    private IPicker iPicker = null;

    public event Action OnShoot = null;

    private void Awake()
    {
        iHand = GetComponent<IHand>();
        iFace = GetComponent<IFace>();
        iPicker = GetComponent<IPicker>();
    }

    private void OnEnable()
    {
        iPicker.OnPicked += OnPicked;
    }

    private void Start()
    {
        weaponData = defaultWeaponData;
    }

    private void Update()
    {
        if(shootDelay > 0f)
        {
            shootDelay -= Time.deltaTime;
        }
    }

    private void OnDisable()
    {
        iPicker.OnPicked -= OnPicked;
    }

    private void OnPicked(IPickable iPickable)
    {
        if ((iPickable as MonoBehaviour).TryGetComponent(out Weapon weapon))
        {
            weaponData = weapon.WeaponData;
        }
    }

    public void Shoot()
    {
        if (weaponData == null)
            return;

        if (!skipCooldown && shootDelay > 0f)
            return;

        shootDelay = weaponData.ProjectileCooldown;

        ObjectsPoolType projectileOPT = weaponData.ProjectileData.ObjectsPoolType;
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
