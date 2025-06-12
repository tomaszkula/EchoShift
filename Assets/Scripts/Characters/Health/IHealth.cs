using System;

public interface IHealth
{
    float MaxHealth { get; }
    float Health { get; }
    event Action<float> OnHealthChanged;
    event Action<float> OnMaxHealthChanged;
    bool Heal(float healthToHeal);
    bool TakeHealth(float healthToTake);
}
