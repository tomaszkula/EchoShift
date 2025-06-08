using UnityEngine;

public class Weapon : MonoBehaviour, IPickable
{
    [Header("References")]
    [SerializeField] private WeaponData weaponData = null;

    public WeaponData WeaponData => weaponData;

    public void Pick()
    {
        Destroy(gameObject);
    }
}
