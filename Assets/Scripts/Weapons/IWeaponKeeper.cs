using System;
using UnityEngine;

public interface IWeaponKeeper
{
    WeaponData WeaponData { get; }
    Vector3 BarrelPosition { get; }
    void SetWeapon(WeaponData weapon);
    event Action<WeaponData> OnWeaponChanged;
}
