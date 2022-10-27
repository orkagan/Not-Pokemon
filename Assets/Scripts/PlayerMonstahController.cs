using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMonstahController : MonoBehaviour
{
    public Monstah monstah;
    private void Start()
    {
        monstah = GetComponent<Monstah>();
    }

    public void Damage(int dmgAmount) { monstah.Damage(dmgAmount); }
    public void Heal(int healAmount) { monstah.Damage(-healAmount); }
}
