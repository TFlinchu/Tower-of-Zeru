using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;

    public void SetMaxHealth(int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        healthText.text = $"{maxHealth}/{healthSlider.maxValue}";
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health;
        healthText.text = $"{health}/{healthSlider.maxValue}";
    }
}
