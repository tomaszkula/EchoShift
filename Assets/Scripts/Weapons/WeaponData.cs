using UnityEngine;

[CreateAssetMenu(fileName = "weaponData_Default", menuName = "New WeaponData", order = 100000)]
public class WeaponData : ScriptableObject
{
    [SerializeField] private ObjectsPoolType_Weapon objectsPoolType = null;
    [SerializeField] private ProjectileData projectileData = null;
    [SerializeField] private float projectileCooldown = 0.1f;
    [SerializeField] private int projectilesCount = 1;
    [SerializeField] private Vector2 projectilesSpawnRange = Vector2.zero;

    public ObjectsPoolType_Weapon ObjectsPoolType => objectsPoolType;
    public ProjectileData ProjectileData => projectileData;
    public float ProjectileCooldown => projectileCooldown;
    public int ProjectilesCount => projectilesCount;
    public Vector2 ProjectilesSpawnRange => projectilesSpawnRange;
}
