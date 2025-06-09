using UnityEngine;

[CreateAssetMenu(fileName = "objectsPoolType_Default", menuName = "ObjectsPoolType/New General", order = 100000)]
public class ObjectsPoolType : ScriptableObject
{
    [SerializeField] private int spawnCount = 1;

    public int SpawnCount => spawnCount;
}
