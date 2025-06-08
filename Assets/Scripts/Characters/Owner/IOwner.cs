using System;
using UnityEngine;

public interface IOwner
{
    GameObject Owner { get; set; }
    event Action<GameObject> OnOwnerChanged;
}
