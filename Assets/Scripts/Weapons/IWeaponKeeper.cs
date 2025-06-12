using System;

public interface IWeaponKeeper
{
    WeaponData WeaponData { get; }
    void SetWeapon(WeaponData weapon);
    event Action<WeaponData> OnWeaponChanged;
}
