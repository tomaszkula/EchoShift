using UnityEngine;
using UnityEngine.UI;

public class CharacterHud : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Character character = null;
    [Space]
    [SerializeField] private Color highHealthColor = Color.green;
    [SerializeField] private Color mediumHealthColor = Color.yellow;
    [SerializeField] private Color lowHealthColor = Color.red;

    [Header("References")]
    [SerializeField] private Slider healthSlider = null;
    [SerializeField] private Image healthSliderFillImage = null;

    private void OnEnable()
    {
        character.IHealth.OnMaxHealthChanged += OnMaxHealthChanged;
        character.IHealth.OnHealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        character.IHealth.OnMaxHealthChanged -= OnMaxHealthChanged;
        character.IHealth.OnHealthChanged -= OnHealthChanged;
    }

    private void OnMaxHealthChanged(float maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        RefreshSlider();
    }

    private void OnHealthChanged(float health)
    {
        healthSlider.value = health;
        RefreshSlider();
    }

    private void RefreshSlider()
    {
        float healthRatio = healthSlider.value / healthSlider.maxValue;
        if(healthRatio > 0.75f)
            healthSliderFillImage.color = highHealthColor;
        else if(healthRatio > 0.25f)
            healthSliderFillImage.color = mediumHealthColor;
        else
            healthSliderFillImage.color = lowHealthColor;
    }
}
