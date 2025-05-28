using System;
using UnityEngine;

public interface IShoot
{
    void Shoot();
    event Action OnShoot;
}
