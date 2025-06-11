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
        set
        {
            maxHealth = value;
            OnMaxHealthChanged?.Invoke(maxHealth);
        }
    }

    private float health = 0f;
    public float Health
    {
        get => health;
        set
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
}
