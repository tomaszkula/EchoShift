using System;
using UnityEngine;

public class OwnerDefault : MonoBehaviour, IOwner
{
    [Header("Settings")]
    [SerializeField] private GameObject defaultOwner = null;

    private GameObject owner = null;
    public GameObject Owner
    {
        get => owner;
        set
        {
            owner = value;
            OnOwnerChanged?.Invoke(owner);
        }
    }

    public event Action<GameObject> OnOwnerChanged = null;

    private void Awake()
    {
        Owner = defaultOwner;
    }
}
