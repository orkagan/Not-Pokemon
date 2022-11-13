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
        Bored, //hp>=50
        Amused, //hp<50
        //Phase 2 second health bar
        Serious, //hp>=50
        AllOut, //hp<50
        Dead
    }
    [SerializeField] private State _state;
    public Monstah monstah;
    private void Start()
    {
        monstah = GetComponent<Monstah>();
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
                break;
            case State.Dead:
                StartCoroutine(DeadState());
                break;
            default:
                Debug.Log("404 State not found, State does not exist in NextState() function, stopping statemachine \n_state: " + _state);
                break;
        }
    }
    private IEnumerator BoredState()
    {
        Debug.Log("State: " + _state);
        while (_state == State.Bored)
        {
            //state transitions
            if (monstah.health < 50) _state = State.Amused;


            yield return null;
        }
        NextState();
    }

    private IEnumerator AmusedState()
    {
        Debug.Log("State: "+_state);
        while (_state == State.Amused)
        {
            //state transitions
            if (monstah.health >= 50) _state = State.Bored;
            if (monstah.health <= 0) _state = State.Serious;

            
            yield return null;
        }
        NextState();
    }
    private IEnumerator SeriousState()
    {
        Debug.Log("State: " + _state);
        //do stuff to setup second phase
        monstah.maxHealth *= 2;
        monstah.Heal(monstah.maxHealth);
        monstah.healthSlider.transform.localScale *= 2;

        while (_state == State.Serious)
        {
            //state transitions
            if (monstah.health < 50) _state = State.AllOut;

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

            yield return null;
        }
        NextState();
    }
    private IEnumerator DeadState()
    {
        Debug.Log("State: " + _state);
        yield return null;
        NextState();
    }

    public void Damage(int dmgAmount) { monstah.Damage(dmgAmount); }
    public void Heal(int healAmount) {monstah.Damage(-healAmount); }
}