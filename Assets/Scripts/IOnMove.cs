using System;
using UnityEngine;

namespace Game
{
    public interface IOnMove
    {
        Action<Vector2> OnMove { get; set; }
    }
}