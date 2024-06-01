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
    private HealthBar healthBar;

    public float CurrentPlague => currentPlague;
    public float MaxPlague => maxPlague;

    private void Start()
    {
        healthBar = FindObjectOfType<HealthBar>();
        InitializePlagueBar();
        StartCoroutine(RegenPlague());
    }

    private void InitializePlagueBar()
    {
        currentPlague = maxPlague;
        plagueSlider.maxValue = maxPlague;
        UpdatePlagueUI();
    }

    private IEnumerator RegenPlague()
    {
        while (true)
        {
            if (healthBar.isDead == false)
                yield break;

            currentPlague = Mathf.Min(currentPlague + plagueRegenRate * Time.deltaTime, maxPlague);
            UpdatePlagueUI();

            yield return null;
        }
    }

    private void UpdatePlagueUI()
    {
        plagueSlider.value = currentPlague;
        plagueText.text = $"{Mathf.FloorToInt(currentPlague)} / {plagueSlider.maxValue}";
    }

    public void ConsumePlague(float amount)
    {
        currentPlague -= amount;
        UpdatePlagueUI();
    }
}