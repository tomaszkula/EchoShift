using UnityEngine;

public interface IFeet
{
    Transform Feet { get; }
    bool IsGrounded();
}
