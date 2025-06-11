using System;

public interface IHealth
{
    float MaxHealth { get; }
    float Health { get; set; }
    event Action<float> OnHealthChanged;
    event Action<float> OnMaxHealthChanged;
}
