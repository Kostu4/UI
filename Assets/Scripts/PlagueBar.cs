using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlagueBar : MonoBehaviour
{
    [SerializeField] private Slider plagueSlider; // Слайдер для отображения порчи
    [SerializeField] private TMP_Text plagueText; // Текст для отображения порчи
    [SerializeField] private float plagueRegenRate = 5f; // Скорость восстановления порчи (ед/сек)
    [SerializeField] private float maxPlague = 100f; // Максимальное количество порчи

    private float currentPlague;

    private NEW_HealthBar healthBar;

    public float CurrentPlague => currentPlague;
    public float MaxPlague => maxPlague;

    private void Start()
    {
        healthBar = FindObjectOfType<NEW_HealthBar>();
        currentPlague = maxPlague;
        plagueSlider.maxValue = maxPlague;
        UpdatePlagueUI();
        StartCoroutine(RegenPlague());
    }

    private IEnumerator RegenPlague()
    {
        while (true)
        {
            if (healthBar.CurrentHealth <= 0)
            { 
                yield break;
            }
            if (currentPlague < maxPlague)
            {
                currentPlague += plagueRegenRate * Time.deltaTime;
                currentPlague = Mathf.Min(currentPlague, maxPlague);
                UpdatePlagueUI();
            }
            yield return null;
        }
    }

    private void UpdatePlagueUI()
    {
        plagueSlider.value = currentPlague;
        plagueText.text = Mathf.FloorToInt(currentPlague).ToString() + " / " + plagueSlider.maxValue;
    }

    public void ConsumePlague(float amount)
    {
        currentPlague -= amount;
        UpdatePlagueUI();
    }
}