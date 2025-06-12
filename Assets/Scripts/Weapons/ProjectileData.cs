using UnityEngine;

[CreateAssetMenu(fileName = "projectileData_Default", menuName = "New ProjectileData", order = 100000)]
public class ProjectileData : ScriptableObject
{
    [SerializeField] private ObjectsPoolType_Projectile objectsPoolType = null;
    [SerializeField] private DamageType damageType = null;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;

    public ObjectsPoolType_Projectile ObjectsPoolType => objectsPoolType;
    public DamageType DamageType => damageType;
    public float Damage => damage;
    public float Speed => speed;
    public float Lifetime => lifetime;
}
