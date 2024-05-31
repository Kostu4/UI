using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NEW_HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar; // Слайдер для отображения порчи
    [SerializeField] private TMP_Text healthText; // Текст для отображения порчи
    [SerializeField] private Image fillBar;
    [SerializeField] private Gradient changeColor;
    [SerializeField] private float healthRegenRate = 3f; // Скорость восстановления порчи (ед/сек)
    [SerializeField] private float maxHealth = 100f; // Максимальное количество порчи

    private float currentHealth;
    public float CurrentHealth => currentHealth;
    public float MaxPlague => maxHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        fillBar.color = changeColor.Evaluate(1f);
        UpdateHealthUI();
        StartCoroutine(RegenHealth());
    }

    private IEnumerator RegenHealth()
    {
        while (currentHealth >0)
        {
            if (currentHealth < maxHealth)
            {
                currentHealth += healthRegenRate * Time.deltaTime;
                currentHealth = Mathf.Min(currentHealth, maxHealth);
                UpdateHealthUI();
            }
            yield return null;
        }
    }

    private void UpdateHealthUI()
    {
        healthBar.value = currentHealth;
        fillBar.color = changeColor.Evaluate(healthBar.normalizedValue);
        healthText.text = Mathf.FloorToInt(currentHealth).ToString() + " / " + healthBar.maxValue;
    }

    public void ConsumeHealth(float amount)
    {
        currentHealth -= amount;
        if (currentHealth - amount < 0)
        { 
            currentHealth = 0;
            Debug.LogWarning("YOU ARE DEAD");
        } 
        UpdateHealthUI();
    }
}
