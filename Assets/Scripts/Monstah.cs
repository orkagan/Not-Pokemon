using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monstah : MonoBehaviour
{
    //Stat variables
    public int health = 100;
    public int maxHealth = 100;
    public bool shielded = false;

    //UI object references
    public Slider healthSlider;
    public Text nameText;
    public Text healthText;

    public void Start()
    {
        nameText.text = name;
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
        if (shielded)
        {
            shielded = false;
        }
        else
        {
            health -= dmgAmount;
            health = Mathf.Clamp(health, 0, maxHealth);
        }
        UpdateUI();
    }

    public void Heal(int healAmount)
    {
        Damage(-healAmount);
    }
    public void Shield()
    {
        shielded = true;
    }

    public void UpdateUI()
    {
        healthText.text = $"HP: {health}/{maxHealth}";
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }
}
