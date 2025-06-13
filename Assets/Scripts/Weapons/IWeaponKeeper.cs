using System;
using UnityEngine;

public interface IWeaponKeeper
{
    WeaponData WeaponData { get; }
    GameObject WeaponInHand { get; }
    void SetWeapon(WeaponData weapon);
    event Action<WeaponData> OnWeaponChanged;
}
