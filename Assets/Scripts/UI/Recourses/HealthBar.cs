using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar; // Слайдер для отображения очков HP
    [SerializeField] private TMP_Text healthText; // Текст для отображения HP
    [SerializeField] private Image fillBar; // Заполнение индикатора HP
    [SerializeField] private Gradient changeColor; // Градиент для изменения цвета заполнения
    [SerializeField] private float healthRegenRate = 1f; // Скорость восстановления HP (ед/сек)
    [SerializeField] private float maxHealth = 100f; // Максимальное количество HP
    [SerializeField] private TMP_Text PlayerDieText;
    public bool isDead = false;
    private float currentHealth;
    public float CurrentHealth => currentHealth;
    public float MaxPlague => maxHealth;

    private void Start()
    {
        //FindObjectOfType<DeathScreen>().HideDeathText();
        InitializeHealthBar();
        StartCoroutine(RegenHealth());
    }

    private void InitializeHealthBar()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        fillBar.color = changeColor.Evaluate(1f);
        UpdateHealthUI();
    }

    private IEnumerator RegenHealth()
    {
        while (isDead == false)
        {
            currentHealth = Mathf.Min(currentHealth + healthRegenRate * Time.deltaTime, maxHealth);
            UpdateHealthUI();

            
            
            yield return null;
        }
    }

    private void UpdateHealthUI()
    {
        healthBar.value = currentHealth;
        fillBar.color = changeColor.Evaluate(healthBar.normalizedValue);
        healthText.text = $"{Mathf.FloorToInt(currentHealth)} / {healthBar.maxValue}";
    }

    public void ConsumeHealth(float amount)
    {
        currentHealth -= amount;
        if (currentHealth - amount < 0)
        {
            isDead = true;
            Debug.LogWarning("YOU ARE DEAD!!!");
            currentHealth = 0;
        }
        UpdateHealthUI();
    }
}
