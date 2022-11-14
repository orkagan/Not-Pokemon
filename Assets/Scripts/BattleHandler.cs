using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHandler : MonoBehaviour
{
    public Text battleLog;
    
    //Game state enum
    GameState _gameState;
    enum GameState
    {
        Setup,
        PlayerTurn,
        EnemyTurn,
        Win,
        Lose
    }

    ///Singleton Setup
    private static BattleHandler _instance;
    public static BattleHandler Instance
    {
        get => _instance;
        private set
        {
            //check if instance of this class already exists and if so keep orignal existing instance
            if (_instance == null)
            {
                _instance = value;
            }
            else if (_instance != value)
            {
                Debug.Log($"{nameof(BattleHandler)} instance already exists, destroy the duplicate!");
                Destroy(value);
            }
        }
    }
    private void Awake()
    {
        Instance = this; //sets the static class instance
    }

    void Start()
    {
        _gameState = GameState.Setup;
    }

    void NextState()
    {
        switch (_gameState)
        {
            case GameState.Setup:
                break;
            case GameState.PlayerTurn:
                break;
            case GameState.EnemyTurn:
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
            default:
                break;
        }
    }

    IEnumerator Setup()
    {
        
        yield return null;
    }
}
