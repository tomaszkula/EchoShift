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

    public void Heal(float healthToHeal)
    {
        if (Health + healthToHeal > MaxHealth)
        {
            Health = MaxHealth;
        }
        else
        {
            Health += healthToHeal;
        }
    }

    public void TakeHealth(float healthToTake)
    {
        if(Health - healthToTake < 0)
        {
            Health = 0;
        }
        else
        {
            Health -= healthToTake;
        }
    }
}
