using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

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
    public void Tackle(Monstah target, int dmgAmount)
    {
        if (target.shielded && dmgAmount > 0)
        {
            target.shielded = false;
            BattleHandler.Instance.battleLog.text = $"{name}'s {MethodBase.GetCurrentMethod().Name} was blocked by a shield.";
        }
        else
        {
            target.Damage(dmgAmount);
            BattleHandler.Instance.battleLog.text = $"{name} used {MethodBase.GetCurrentMethod().Name}.";
        }
        UpdateUI();
        target.UpdateUI();
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
        BattleHandler.Instance.battleLog.text = $"{name} used {MethodBase.GetCurrentMethod().Name}.";
    }
    public void Shield()
    {
        shielded = true;
        BattleHandler.Instance.battleLog.text = $"{name} used {MethodBase.GetCurrentMethod().Name}.";
        UpdateUI();
    }
    public void Charge(Monstah target, int dmgAmount)
    {
        if (!charged)
        {
            charged = true;
            BattleHandler.Instance.battleLog.text = $"{name} starts charging an attack.";
        }
        else
        {
            if (target.shielded && dmgAmount > 0)
            {
                target.shielded = false;
                BattleHandler.Instance.battleLog.text = $"{name}'s {MethodBase.GetCurrentMethod().Name} was blocked by a shield.";
                target.UpdateUI();
            }
            else
            {
                target.Damage(dmgAmount);
                BattleHandler.Instance.battleLog.text = $"{name} fires a charged beam.";
            }
            charged = false;
        }
        UpdateUI();
        target.UpdateUI();
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
