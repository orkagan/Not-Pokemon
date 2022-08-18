using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterAI : MonoBehaviour
{
    public Text healthText;
    public Slider healthSlider;
    public int health = 20;
    public int maxHealth = 20;

    public void Start()
    {
        healthSlider.maxValue = maxHealth;
    }

    public void Damage(int dmgAmount)
    {
        health -= dmgAmount;
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateUI();
    }

    public void Heal(int healAmount)
    {
        Damage(-healAmount);
    }

    public void UpdateUI()
    {
        healthText.text = $"Health: {health}/{maxHealth}";
        healthSlider.value = health;
    }
}
