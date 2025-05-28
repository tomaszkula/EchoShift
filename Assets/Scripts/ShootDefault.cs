using System;
using UnityEngine;

public class ShootDefault : MonoBehaviour, IShoot
{
    [Header("Settings")]
    [SerializeField] private GameObject projectilePrefab = null;
    [SerializeField] private float projectileCooldown = 0.5f;

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
        if (shootDelay > 0f)
            return;

        shootDelay = projectileCooldown;

        Quaternion projectileRotation  = iFace.FaceDirection switch
        {
            Direction.Right => Quaternion.Euler(0, 0, 0),
            Direction.Left => Quaternion.Euler(0, 180, 0),
            _ => Quaternion.identity
        };
        GameObject projectile = Instantiate(projectilePrefab, iHand.Hand.position, projectileRotation);

        OnShoot?.Invoke();
    }
}
