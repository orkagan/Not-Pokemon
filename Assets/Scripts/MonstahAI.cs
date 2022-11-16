using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Monstah))] //Need a monstah script for AI to run on
public class MonstahAI : MonoBehaviour
{
    public int
        tackleDmg = 5,
        chargeDmg = 15,
        healAmt = 5;
    public enum State
    {
        //Phase 1
        Bored,      //hp >= maxHealth/2
        Amused,     //hp < maxHealth/2
        //Phase 2 second health bar
        Serious,    //hp >= maxHealth/2
        AllOut,     //hp < maxHealth/2
        Dead        //hp <= 0
    }
    [SerializeField] private State _state;
    public Monstah monstah;
    public GameObject player;
    Monstah _playerMon;

    public Vector3 rotateSpeed = new Vector3(5, 10, 3);
    public float rotateSpeedMax = 30;
    public bool phase2 = false;
    private void Start()
    {
        monstah = GetComponent<Monstah>();
        _playerMon = player.GetComponent<Monstah>();
        GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        phase2 = false;
    }
    public void NextState()
    {
        switch (_state)
        {
            case State.Bored:
                StartCoroutine(BoredState());
                break;
            case State.Amused:
                StartCoroutine(AmusedState());
                break;
            case State.Serious:
                StartCoroutine(SeriousState());
                break;
            case State.AllOut:
                StartCoroutine(AllOutState());
                break;
            case State.Dead:
                StartCoroutine(DeadState());
                break;
            default:
                Debug.Log("404 State not found stopping statemachine \n_state: " + _state);
                break;
        }
    }
    private IEnumerator BoredState()
    {
        Debug.Log("State: " + _state);
        //state transitions
        if (monstah.health < monstah.maxHealth / 2) { _state = State.Amused; NextState(); yield break; }

        //make move
        ChooseMove(4,2,0,0);
        yield return new WaitForSeconds(2); // delay to show off enemy move

        BattleHandler.Instance.battleLog.text += $"\n{monstah.name} is looking bored.";
        BattleHandler.Instance.EnemyTurnOver();
        yield return null;
    }

    private IEnumerator AmusedState()
    {
        Debug.Log("State: "+_state);
        //state transitions
        if (monstah.health >= monstah.maxHealth / 2) { _state = State.Bored; NextState(); yield break; }
        if (monstah.health <= 0) { _state = State.Serious; NextState(); yield break; }

        //make move
        ChooseMove(4, 2, 1, 0);
        yield return new WaitForSeconds(2); // delay to show off enemy move

        BattleHandler.Instance.battleLog.text += $"\n{monstah.name} is looking amused.";
        BattleHandler.Instance.EnemyTurnOver();
        yield return null;
    }
    private IEnumerator SeriousState()
    {
        Debug.Log("State: " + _state);
        //transition into second phase if not already in
        if (!phase2)
        {
            BattleHandler.Instance.battleLog.text = $"{name} defeated."; //fake win
            yield return new WaitForSeconds(1); //short delay fake out
            StartPhase2();
            yield return new WaitForSeconds(4);
        }
        //state transitions
        if (monstah.health < monstah.maxHealth/2) { _state = State.AllOut; NextState(); yield break; }

        //make move
        ChooseMove(3, 1, 1, 3);
        yield return new WaitForSeconds(2); // delay to show off enemy move

        BattleHandler.Instance.battleLog.text += $"\n{monstah.name} is getting serious.";
        BattleHandler.Instance.EnemyTurnOver();
        yield return null;
    }
    private IEnumerator AllOutState()
    {
        Debug.Log("State: " + _state);
        
        //state transitions
        if (monstah.health <= 0) { _state = State.Dead; NextState(); yield break; }

        //every turn enemy does a bit more damage
        tackleDmg+=1;
        chargeDmg+=2;
        //make move
        ChooseMove(3, 0, 0, 5); //enemy stops healing altogether (by having weights for heal and shield be 0)
        yield return new WaitForSeconds(2); // delay to show off enemy move

        BattleHandler.Instance.battleLog.text += $"\n{monstah.name} is going all out!";
        BattleHandler.Instance.EnemyTurnOver();
        yield return null;
    }
    private IEnumerator DeadState()
    {
        Debug.Log("State: " + _state);
        //add physics for a quick death "animation"
        if (GetComponent<Rigidbody>() == null)
        {
            Rigidbody phse2objRB = gameObject.AddComponent<Rigidbody>();
            phse2objRB.AddForce(50,150,-100);
            //BattleHandler end game stuff
            Destroy(gameObject, 3);
        }
        yield return null;
    }
    private void Update()
    {
        //dumb code to make qube start spinning
        if(phase2 && _state!=State.Dead)
        {
            transform.Rotate(rotateSpeed * Time.deltaTime);
            if (rotateSpeed.x < rotateSpeedMax && _state==State.Serious) rotateSpeed *= 1.01f;
            if (rotateSpeed.x < rotateSpeedMax * 10 && _state == State.AllOut) rotateSpeed *= 1.05f;
        }
    }
    
    public void ChooseMove(int[] weights)
    {
        //sanity check weights
        if (monstah.charged) //if charged previous turn
        {
            monstah.Charge(_playerMon, chargeDmg); //then fire beam
        }
        if (monstah.maxHealth - monstah.health < healAmt) //if heal is pointless
        {
            weights[1] = 0; //set heal weight to 0
        }
        if (monstah.shielded) //if already shielded
        {
            weights[2] = 0; //set shield weight to 0
        }

        //figure out the total weight and make a roll between 1 and total weight
        int totalWeight = 0;
        foreach (int i in weights) totalWeight += i;
        int roll = Random.Range(1, totalWeight+1);

        //so it kinda works like a dice table ie. if the roll is
        //1 to tackleWeight choose tackle
        //tackleWeight+1 to (cumulative sum)+healWeight choose heal, etc.
        //increments x over weight values until the roll value is found, then sets moveIndex
        int moveIndex = 0;
        int x = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            if (weights[i] == 0) continue;
            x += weights[i];
            if (roll <= x)
            {
                moveIndex = i;
                break;
            }
        }
        //do the move based on the chosen number
        switch (moveIndex)
        {
            case 0:
                monstah.Tackle(_playerMon, tackleDmg);
                break;
            case 1:
                monstah.Heal(healAmt);
                break;
            case 2:
                monstah.Shield();
                break;
            case 3:
                monstah.Charge(_playerMon, chargeDmg);
                break;
            default:
                break;
        }
    }

    //overload that's easier to look at lol
    public void ChooseMove(int tackleWeight, int healWeight, int shieldWeight, int chargeWeight)
    {
        ChooseMove(new int[4] { tackleWeight,healWeight,shieldWeight,chargeWeight });
    }

    public void StartPhase2()
    {
        //change values
        tackleDmg *= 2;
        chargeDmg *= 2;
        healAmt *= 2;
        monstah.maxHealth *= 2;
        monstah.Heal(monstah.maxHealth);

        //UI stuff
        //make health bar bigger
        RectTransform rt = (RectTransform)monstah.healthSlider.transform;
        rt.sizeDelta *= 2;
        //change font
        Font dsFont = Resources.Load<Font>("Fonts/OptimusPrinceps");
        monstah.healthText.font = dsFont;
        monstah.healthText.fontSize = 20;
        monstah.nameText.font = dsFont;
        monstah.nameText.fontSize = 25;
        name = "Qube of the Third Dimension";
        monstah.nameText.text = monstah.name;
        monstah.UpdateUI();
        //add shadows
        GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        //battle log message
        BattleHandler.Instance.battleLog.text = $"Wait what?! Enemy Skware was actually a {monstah.name}!";
        phase2 = true;
    }
}