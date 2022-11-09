using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monstah : MonoBehaviour
{
    //Stat variables
    public int health = 100;
    public int maxHealth = 100;

    //UI object references
    public Text healthText;
    public Text nameText;
    public Slider healthSlider;

    public void Start()
    {
        nameText.text = name;
        healthSlider.maxValue = maxHealth;
        UpdateUI();
    }

    public void Update()
    {

    }
    public void Attack(Monstah target, int dmgAmount)
    {
        target.Damage(dmgAmount);
        UpdateUI();
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
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }
}
