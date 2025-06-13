using System;
using UnityEngine;

public interface IHand
{
    Transform Hand { get; }
    event Action<Transform> OnHandChanged;
}
