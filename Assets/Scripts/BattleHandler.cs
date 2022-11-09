using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    GameState _gameState;
    enum GameState
    {
        Start,
        PlayerTurn,
        EnemyTurn,
        Win,
        Lose
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
