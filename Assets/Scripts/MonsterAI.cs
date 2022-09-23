using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterAI : MonoBehaviour
{
    public Text healthText;
    public Text nameText;
    public Slider healthSlider;
    public int health = 20;
    public int maxHealth = 20;
    public MonState monState;

    public void Start()
    {
        healthSlider.maxValue = maxHealth;
        nameText.text = $"{gameObject.name}";
        UpdateUI();
    }

    public void Update()
    {
        switch (monState)
        {
            case MonState.Aggressive:
                break;
            case MonState.Defensive:
                break;
            case MonState.Dead:
                break;
            default:
                break;
        }
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
        healthText.text = $"HP: {health}/{maxHealth}";
        healthSlider.value = health;
    }
}

public enum MonState
{
    Aggressive,
    Defensive,
    Dead
}