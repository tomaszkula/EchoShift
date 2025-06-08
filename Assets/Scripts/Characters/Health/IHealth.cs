using System;

public interface IHealth
{
    float Health { get; set; }
    event Action<float> OnHealthChanged;
}
