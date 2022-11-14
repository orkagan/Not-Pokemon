using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Monstah))] //Need a monstah script for player to control
public class PlayerMonstahController : MonoBehaviour
{
    public Monstah _monstah;
    public GameObject target;
    Monstah _targetMon;
    private void Start()
    {
        _monstah = GetComponent<Monstah>();
        _targetMon = target.GetComponent<Monstah>();
    }

    public void Tackle(int dmgAmount) 
    {
        _monstah.Attack(_targetMon, dmgAmount);
    }
    public void Heal(int healAmount) 
    {
        _monstah.Heal(healAmount);
    }
    public void Shield()
    {
        _monstah.Shield();
    }
}
