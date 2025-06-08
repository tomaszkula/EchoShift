using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject spawner = null;

    public Vector2 SpawnPosition => spawner.transform.position;

    private void Awake()
    {
        Manager.Instance.GetManager<PlayerManager>().SpawnPosition = SpawnPosition;
    }
}
