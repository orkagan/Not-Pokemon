using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Monstah))] //Need a monstah script for AI to run on
public class MonstahAI : MonoBehaviour
{
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
    public Vector3 rotateSpeed = new Vector3(5, 10, 3);
    public float rotateSpeedMax = 30;
    public bool phase2 = false;
    private void Start()
    {
        monstah = GetComponent<Monstah>();
        phase2 = false;
        NextState();
    }
    private void NextState()
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
                BattleHandler.Instance.battleLog.text = $"{monstah.name} is going all out!";
                break;
            case State.Dead:
                StartCoroutine(DeadState());
                BattleHandler.Instance.battleLog.text = $"{monstah.name} defeated. You Win!";
                break;
            default:
                Debug.Log("404 State not found, State does not exist in NextState() function, stopping statemachine \n_state: " + _state);
                break;
        }
    }
    private IEnumerator BoredState()
    {
        Debug.Log("State: " + _state);
        BattleHandler.Instance.battleLog.text = $"{monstah.name} is looking bored.";
        while (_state == State.Bored)
        {
            //state transitions
            if (monstah.health < monstah.maxHealth/2) _state = State.Amused;


            yield return null;
        }
        NextState();
    }

    private IEnumerator AmusedState()
    {
        Debug.Log("State: "+_state);
        BattleHandler.Instance.battleLog.text = $"{monstah.name} is looking amused.";
        while (_state == State.Amused)
        {
            //state transitions
            if (monstah.health >= monstah.maxHealth/2) _state = State.Bored;
            if (monstah.health <= 0) _state = State.Serious;

            
            yield return null;
        }
        NextState();
    }
    private IEnumerator SeriousState()
    {
        Debug.Log("State: " + _state);
        ///do stuff to setup second phase
        if (!phase2)
        {
            //change values
            monstah.maxHealth *= 3;
            monstah.Heal(monstah.maxHealth);

            //UI stuff
            //make health bar bigger
            RectTransform rt = (RectTransform)monstah.healthSlider.transform;
            rt.sizeDelta *= 2;
            //change font
            Font dsFont = Resources.Load<Font>("Fonts/OptimusPrinceps");
            monstah.healthText.font = dsFont;
            monstah.nameText.font = dsFont;
            monstah.nameText.fontSize = 25;
            name = "Boss Qube of the Third Dimension";
            monstah.nameText.text = monstah.name;
            monstah.UpdateUI();
            //battle log message
            BattleHandler.Instance.battleLog.text = $"What? Enemy Skware was actually a {monstah.name}!";
            phase2 = true;
            yield return new WaitForSeconds(3);
        }
        BattleHandler.Instance.battleLog.text = $"{monstah.name} is getting serious.";
        while (_state == State.Serious)
        {
            //state transitions
            if (monstah.health < monstah.maxHealth/2) _state = State.AllOut;

            if (rotateSpeed.x < rotateSpeedMax) rotateSpeed *= 1.05f;
            yield return null;
        }
        NextState();
    }
    private IEnumerator AllOutState()
    {
        Debug.Log("State: " + _state);
        while (_state == State.AllOut)
        {
            //state transitions
            if (monstah.health <= 0) _state = State.Dead;
            if(rotateSpeed.x < rotateSpeedMax * 10) rotateSpeed *= 1.05f;
            yield return null;
        }
        NextState();
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
        NextState();
    }
    private void Update()
    {
        if(phase2 && _state!=State.Dead)
        {
            transform.Rotate(rotateSpeed * Time.deltaTime);
        }
    }
    public void Damage(int dmgAmount) { monstah.Damage(dmgAmount); }
    public void Heal(int healAmount) {monstah.Damage(-healAmount); }
}