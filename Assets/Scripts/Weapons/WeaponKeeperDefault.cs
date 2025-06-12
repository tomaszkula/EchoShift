using System;
using UnityEngine;

public class WeaponKeeperDefault : MonoBehaviour, IWeaponKeeper
{
    [Header("Settings")]
    [SerializeField] private WeaponData defaultWeaponData = null;

    private WeaponData weaponData = null;
    public WeaponData WeaponData
    {
        get => weaponData;
        private set
        {
            weaponData = value;
            OnWeaponChanged?.Invoke(weaponData);
        }
    }

    public event Action<WeaponData> OnWeaponChanged = null;

    private void Start()
    {
        WeaponData = defaultWeaponData;
    }

    public void SetWeapon(WeaponData weapon)
    {
        WeaponData = weapon;
    }
}
