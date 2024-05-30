using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillLashStrike : MonoBehaviour
{
    [SerializeField] private Image abilityImage;
    [SerializeField] private Slider plagueSlider; // Слайдер для отображения порчи
    [SerializeField] private TMP_Text plagueText; // Текст для отображения порчи
    [SerializeField] private float plagueRegenRate = 5f; // Скорость восстановления порчи (ед/сек)
    [SerializeField] private float maxPlague = 100f; // Максимальное кол-во порчи
    [SerializeField] private float abilityCost = 20f; // Стоимость способности
    [SerializeField] private float cooldownTime; // Длительность кулдауна способности

    private float currentPlague;
    private bool isOnCooldown = false;

        private void Start()
        {
            currentPlague = maxPlague;
            plagueSlider.maxValue = maxPlague;
            UpdateManaUI();
            StartCoroutine(RegenMana());
        }


    public void UseAbility()
    {
        if (!isOnCooldown && currentPlague >= abilityCost)
        {
            abilityImage.fillAmount = 0;
            currentPlague -= abilityCost;
            UpdateManaUI();
            StartCoroutine(CooldownRoutine());
        }
    }

    private IEnumerator CooldownRoutine()
    {
        isOnCooldown = true;
        float elapsedTime = 0f;
        while (elapsedTime < cooldownTime)
        {
            elapsedTime += Time.deltaTime;
            abilityImage.fillAmount += Time.deltaTime;
            yield return null;
        }
        isOnCooldown = false;
    }

    private IEnumerator RegenMana()
    {
        while (true)
        {
            if (currentPlague < maxPlague)
            {
                currentPlague += plagueRegenRate * Time.deltaTime;
                currentPlague = Mathf.Min(currentPlague, maxPlague);
                UpdateManaUI();
            }
            yield return null;
        }
    }

    private void UpdateManaUI()
    {
        plagueSlider.value = currentPlague;
        plagueText.text = Mathf.FloorToInt(currentPlague).ToString();
    }
}
