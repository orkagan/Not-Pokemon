using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Monstah))]
public class MonStateMachine : MonoBehaviour
{
    public enum State
    {
        Bored,
        Amused,
        Serious,
        AllOut,
        Dead
    }

    [SerializeField] private State _state;

    //private Monstah _monstaht;
    private void Start()
    {
        //_monstah = GetComponent<Monstah>();

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
        yield return null; //stick this in a while loop
        NextState();
    }

    private IEnumerator AmusedState()
    {
        yield return null;
        NextState();
    }
    private IEnumerator SeriousState()
    {
        yield return null;
        NextState();
    }
    private IEnumerator AllOutState()
    {
        yield return null;
        NextState();
    }
    private IEnumerator DeadState()
    {
        yield return null;
        NextState();
    }
}