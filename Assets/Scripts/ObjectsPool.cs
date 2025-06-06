using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private List<ObjectsPoolsManager.Data> objectsPoolsData = new List<ObjectsPoolsManager.Data>();

    private ObjectsPoolsManager objectsPoolsManager = null;

    public List<ObjectsPoolsManager.Data> ObjectsPoolsData => objectsPoolsData;

    public event Action OnUnregistered = null;

    private void Awake()
    {
        objectsPoolsManager = Manager.Instance.GetManager<ObjectsPoolsManager>();

        Register();
    }

    private void OnDestroy()
    {
        Unregister();
    }

    private void Register()
    {
        objectsPoolsManager.Register(this);
    }

    private void Unregister()
    {
        objectsPoolsManager.Unregister(this);

        OnUnregistered?.Invoke();
    }
}
