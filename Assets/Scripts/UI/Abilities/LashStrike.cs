using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LashStrike : MonoBehaviour
{
    [SerializeField] private Image abilityImage; // Иконка способности
    [SerializeField] private float abilityCost = 20f; // Стоимость способности очков порчи
    [SerializeField] private float abilityCostHealth = 15f; // Стоимость способности очков HP
    [SerializeField] private float cooldownTime; // Длительность кулдауна способности

    private PlagueBar plagueBar;
    private bool isOnCooldown = false;

    private HealthBar healthBar;

    private void Start()
    {
        healthBar = FindObjectOfType<HealthBar>();
        plagueBar = FindObjectOfType<PlagueBar>();
        abilityImage.fillAmount = 1;
    }

    public void UseAbility()
    {
        if (!isOnCooldown && plagueBar.CurrentPlague >= abilityCost && healthBar.CurrentHealth >= abilityCostHealth)
        {
            healthBar.ConsumeHealth(abilityCostHealth);
            plagueBar.ConsumePlague(abilityCost);
            StartCoroutine(CooldownRoutine());
        }
    }

    private IEnumerator CooldownRoutine()
    {
        isOnCooldown = true;
        abilityImage.fillAmount = 0;
        float elapsedTime = 0f;
        while (elapsedTime < cooldownTime)
        {
            elapsedTime += Time.deltaTime;
            abilityImage.fillAmount = Mathf.Clamp01(elapsedTime / cooldownTime);
            yield return null;
        }
        isOnCooldown = false;
    }
}
