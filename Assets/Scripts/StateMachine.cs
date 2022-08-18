using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AiAgent))]
public class StateMachine : MonoBehaviour
{
    public enum State
    {
        Patrol, 
        Chase, 
        Hunt,
    }

    [SerializeField] private State _state;

    private AiAgent _aiAgent;
    private void Start()
    {
        _aiAgent = GetComponent<AiAgent>();

        NextState();
    }
    private void NextState()
    {
        switch (_state)
        {
            case State.Patrol:
                StartCoroutine(PatrolState());
                break;
            case State.Chase:
                StartCoroutine(ChaseState());
                break;
            case State.Hunt:
                StartCoroutine(HuntState());
                break;
            default:
                Debug.Log("404 State not found, State does not exist in NextState function, stopping statemachine \n_state: " + _state);
                break;
        }
    }
    private IEnumerator PatrolState()
    {
        yield return null;
        NextState();
    }

    private IEnumerator ChaseState()
    {
        yield return null;
        NextState();
    }
    private IEnumerator HuntState()
    {
        yield return null;
        NextState();
    }
}