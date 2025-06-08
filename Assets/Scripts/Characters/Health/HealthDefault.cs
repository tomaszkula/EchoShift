using System;
using UnityEngine;

public class HealthDefault : MonoBehaviour, IHealth
{
    [Header("Settings")]
    [SerializeField] private float maxHealth = 100f;

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

    private void Start()
    {
        health = maxHealth;
    }
}
