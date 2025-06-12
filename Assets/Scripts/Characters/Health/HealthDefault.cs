using System;
using UnityEngine;

public class HealthDefault : MonoBehaviour, IHealth
{
    [Header("Settings")]
    [SerializeField] private float defaultMaxHealth = 100f;

    private float maxHealth = 0f;
    public float MaxHealth
    {
        get => maxHealth;
        private set
        {
            maxHealth = value;
            OnMaxHealthChanged?.Invoke(maxHealth);
        }
    }

    private float health = 0f;
    public float Health
    {
        get => health;
        private set
        {
            health = value;
            OnHealthChanged?.Invoke(health);
        }
    }

    public event Action<float> OnHealthChanged = null;
    public event Action<float> OnMaxHealthChanged = null;

    private void Start()
    {
        MaxHealth = defaultMaxHealth;
        Health = MaxHealth;
    }

    public bool Heal(float healthToHeal)
    {
        if(Health >= MaxHealth)
        {
            return false;
        }

        if (Health + healthToHeal > MaxHealth)
        {
            Health = MaxHealth;
        }
        else
        {
            Health += healthToHeal;
        }

        return true;
    }

    public bool TakeHealth(float healthToTake)
    {
        if (Health <= 0)
        {
            return false;
        }

        if (Health - healthToTake < 0)
        {
            Health = 0;
        }
        else
        {
            Health -= healthToTake;
        }

        return true;
    }
}
