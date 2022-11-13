using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    //Game state enum
    GameState _gameState;
    enum GameState
    {
        Start,
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
    // Start is called before the first frame update
    void Start()
    {
        _gameState = GameState.Start;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
