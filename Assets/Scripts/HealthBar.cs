//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//public class HealthBar : MonoBehaviour
//{
//    [SerializeField] private Slider healthBar;
//    [SerializeField] private TMP_Text healthBarText;
//    [SerializeField] private bool isDead = false;
//    [SerializeField] private Image fillBar;
//    [SerializeField] private Gradient changeColor;
//    [SerializeField] private float healthRegenRate = 3f; // Скорость восстановления порчи (ед/сек)
//    private float currentHealth;

//    public float CurrentHealth => currentHealth;
//    public float MaxHealth => healthBar.maxValue;


//    private void Awake()
//    {
//        healthBar.value = healthBar.maxValue;
//        healthBarText.text = healthBar.value.ToString();
//        fillBar.color = changeColor.Evaluate(1f);
//        StartCoroutine(UpdateHealthBar());
//        StartCoroutine(RegenHealth());
//    }
//    IEnumerator UpdateHealthBar()
//    {
//        Debug.Log("Coroutine is running");
//        while (isDead!=true)
//        {
//            if (healthBar.value <= healthBar.minValue)
//            {
//                isDead = true;
//                Debug.LogWarning("YOU ARE DIED");
//            }
//            healthBarText.text = healthBar.value.ToString() + " / " + healthBar.maxValue.ToString();
//            fillBar.color = changeColor.Evaluate(healthBar.normalizedValue);
//            yield return null;
//        }
//    }

//    private IEnumerator RegenHealth()
//    {
//        while (true)
//        {
//            if (currentHealth < healthBar.maxValue)
//            {
//                currentHealth += healthRegenRate * Time.deltaTime;
//                currentHealth = Mathf.Min(currentHealth, healthBar.maxValue);
//            }
//            yield return null;
//        }
//    }
//}
