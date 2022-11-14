using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monstah : MonoBehaviour
{
    //Stat variables
    public int health = 50;
    public int maxHealth = 50;
    public bool shielded;
    public bool charged;

    //UI object references
    public Slider healthSlider;
    public Text nameText;
    public Text healthText;
    public GameObject shield;

    public void Start()
    {
        nameText.text = name;
        shielded = false;
        charged = false;
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
        if (shielded && dmgAmount>0)
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
        UpdateUI();
    }
    public void Charge(Monstah target, int dmgAmount)
    {
        if (!charged)
        {
            charged = true;
        }
        else
        {
            Attack(target, dmgAmount);
            charged = false;
        }
    }

    public void UpdateUI()
    {
        healthText.text = $"HP: {health}/{maxHealth}";
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
        if (shielded) shield.SetActive(true);
        else shield.SetActive(false);
    }
}
