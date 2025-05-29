using UnityEngine;

public class OwnerDefault : MonoBehaviour, IOwner
{
    [Header("Settings")]
    [SerializeField] private GameObject defaultOwner = null;

    public GameObject Owner { get; set; }

    private void Awake()
    {
        Owner = defaultOwner;
    }
}
