using UnityEngine;

public class Weapon : MonoBehaviour, IPickable
{
    [Header("References")]
    [SerializeField] private WeaponData weaponData = null;

    private IKillable iKillable = null;

    public WeaponData WeaponData => weaponData;

    private void Awake()
    {
        iKillable = GetComponent<IKillable>();
    }

    public void Pick(IPicker iPicker)
    {
        if((iPicker as MonoBehaviour).TryGetComponent(out IWeaponKeeper iWeaponKeeper))
        {
            iWeaponKeeper.SetWeapon(weaponData);
        }

        iKillable?.Kill();
    }
}
