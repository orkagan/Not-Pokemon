using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Monstah))] //Need a monstah script for player to control
public class PlayerMonstahController : MonoBehaviour
{
    public Monstah monstah;
    public GameObject enemy;
    Monstah _enemyMon;
    private void Start()
    {
        monstah = GetComponent<Monstah>();
        _enemyMon = enemy.GetComponent<Monstah>();
    }

    public void Tackle(int dmgAmount) 
    {
        monstah.Attack(_enemyMon, dmgAmount);
        BattleHandler.Instance.NextState();
    }
    public void Heal(int healAmount) 
    {
        monstah.Heal(healAmount);
        BattleHandler.Instance.NextState();
    }
    public void Shield()
    {
        monstah.Shield();
        BattleHandler.Instance.NextState();
    }
    public void Charge(int dmgAmount)
    {
        monstah.Charge(_enemyMon, dmgAmount);
        BattleHandler.Instance.NextState();
    }
    public void Die()
    {
        gameObject.AddComponent<Rigidbody2D>();
        Destroy(gameObject, 3);
    }
}