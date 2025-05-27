using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawner = null;

    public Vector2 spawnPosition => spawner.transform.position;
}
