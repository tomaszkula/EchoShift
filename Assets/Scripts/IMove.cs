using System;
using UnityEngine;

public interface IMove
{
    void Move(Vector2 direction);
    event Action<Vector2> OnMove;
}