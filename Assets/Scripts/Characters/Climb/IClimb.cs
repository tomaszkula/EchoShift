using System;
using UnityEngine;

public interface IClimb
{
    bool IsClimbing { get; }
    void Climb(Vector2 direction);
    event Action<Vector2> OnClimb;
}
