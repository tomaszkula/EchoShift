using UnityEngine;

public class Weapon : MonoBehaviour, IPickable
{
    [SerializeField] private WeaponData weaponData = null;

    public void Pick()
    {
        Destroy(gameObject);
    }
}
