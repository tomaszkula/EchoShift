using System;

public interface IHealth
{
    float MaxHealth { get; }
    float Health { get; }
    event Action<float> OnHealthChanged;
    event Action<float> OnMaxHealthChanged;
    void Heal(float healthToHeal);
    void TakeHealth(float healthToTake);
}
