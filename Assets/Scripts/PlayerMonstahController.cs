using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Monstah))] //Need a monstah script for player to control
public class PlayerMonstahController : MonoBehaviour
{
    public int
        tackleDmg = 10,
        chargeDmg = 30,
        healAmt = 10;
    
    public Monstah monstah;
    public GameObject enemy;
    Monstah _enemyMon;
    private void Start()
    {
        monstah = GetComponent<Monstah>();
        _enemyMon = enemy.GetComponent<Monstah>();
    }

    public void Tackle() 
    {
        monstah.Tackle(_enemyMon, tackleDmg);
        BattleHandler.Instance.PlayerTurnOver();
    }
    public void Heal() 
    {
        monstah.Heal(healAmt);
        BattleHandler.Instance.PlayerTurnOver();
    }
    public void Shield()
    {
        monstah.Shield();
        BattleHandler.Instance.PlayerTurnOver();
    }
    public void Charge()
    {
        monstah.Charge(_enemyMon, chargeDmg);
        BattleHandler.Instance.PlayerTurnOver();
    }
    public void Die()
    {
        gameObject.AddComponent<Rigidbody2D>();
        Destroy(gameObject, 3);
    }
}