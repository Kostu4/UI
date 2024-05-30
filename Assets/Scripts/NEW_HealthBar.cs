using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NEW_HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private TMP_Text healthBarText;
    [SerializeField] private bool isDead = false;
    [SerializeField] private Image fillBar;
    [SerializeField] private Gradient changeColor;

    private void Awake()
    {
        healthBar.value = healthBar.maxValue;
        healthBarText.text = healthBar.value.ToString();
        fillBar.color = changeColor.Evaluate(1f);
        StartCoroutine(UpdateHealthBar());
    }
    IEnumerator UpdateHealthBar()
    {
        Debug.Log("Coroutine is running");
        while (isDead!=true)
        {
            if (healthBar.value <= healthBar.minValue)
            {
                isDead = true;
                Debug.LogWarning("YOU ARE DIED");
            }
            healthBarText.text = healthBar.value.ToString() + " / " + healthBar.maxValue.ToString();
            fillBar.color = changeColor.Evaluate(healthBar.normalizedValue);
            yield return null;
        }
    }
}
