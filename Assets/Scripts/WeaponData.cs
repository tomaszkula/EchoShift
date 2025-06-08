using UnityEngine;

[CreateAssetMenu(fileName = "weaponData_Default", menuName = "New WeaponData", order = 100000)]
public class WeaponData : ScriptableObject
{
    [SerializeField] private float projectileCooldown = 0.1f;

    public float ProjectileCooldown => projectileCooldown;
}
