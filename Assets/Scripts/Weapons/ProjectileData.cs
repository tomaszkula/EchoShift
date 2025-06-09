using UnityEngine;

[CreateAssetMenu(fileName = "projectileData_Default", menuName = "New ProjectileData", order = 100000)]
public class ProjectileData : ScriptableObject
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;
    
    public float Speed => speed;
    public float Lifetime => lifetime;
}
