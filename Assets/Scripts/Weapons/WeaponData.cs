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

    [Header("Weapon In Hand")]
    [SerializeField] private ObjectsPoolType_WeaponInHand wihOPT = null;
    [SerializeField] private Vector3 wihPivot = Vector3.zero;
    [SerializeField] private Vector3 wihMufflePosition = Vector3.zero;

    public ObjectsPoolType_WeaponInHand WihOPT => wihOPT;
    public Vector3 WihPivot => wihPivot;
    public Vector3 WihMufflePosition => wihMufflePosition;

    [Header("Debug")]
    [SerializeField] private float wihMuffleGizmoRadius = 0.05f;

    public float WihMuffleGizmoRadius => wihMuffleGizmoRadius;
}
