using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHandler : MonoBehaviour
{
    public Text battleLog;
    public GameObject[] moveButtObjs;
    List<Button> _moveButts;
    public GameObject player, enemy;
    PlayerMonstahController _player;
    MonstahAI _enemy;
    
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

    #region Singleton Setup
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
    #endregion

    void Start()
    {
        _gameState = GameState.Setup;
        _moveButts = new List<Button>();
        foreach(GameObject obj in moveButtObjs)
        {
            _moveButts.Add(obj.GetComponent<Button>());
            _player = player.GetComponent<PlayerMonstahController>();
            _enemy = enemy.GetComponent<MonstahAI>();
        }
        NextState();
    }

    public void NextState()
    {
        if (_enemy.monstah.health<=0 && _enemy.phase2) _gameState = GameState.Win;
        else if (_player.monstah.health <= 0) _gameState = GameState.Lose;

        switch (_gameState)
        {
            case GameState.Setup:
                StartCoroutine(Setup());
                break;
            case GameState.PlayerTurn:
                StartCoroutine(PlayerTurn());
                break;
            case GameState.EnemyTurn:
                StartCoroutine(EnemyTurn());
                break;
            case GameState.Win:
                StartCoroutine(Win());
                break;
            case GameState.Lose:
                StartCoroutine(Lose());
                break;
            default:
                break;
        }
    }

    IEnumerator Setup()
    {
        //do setup stuff
        ClickableMoveButts(false);
        battleLog.text = $"A wild {enemy.name} has showed up.";
        yield return new WaitForSeconds(3f);
        _gameState = GameState.PlayerTurn;
        NextState();
    }
    IEnumerator PlayerTurn()
    {
        //if charging last turn then do charge attack
        if (_player.monstah.charged)
        {
            _player.Charge();
            PlayerTurnOver();
        }
        else
        {
            ClickableMoveButts(true);
        }
        yield return null;
    }
    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(2); //short delay to show off player's move
        //if charging last turn then do charge attack
        if (_enemy.monstah.charged)
        {
            _enemy.monstah.Charge(_player.monstah, _enemy.chargeDmg);
            EnemyTurnOver();
        }
        else
        {
            _enemy.NextState(); //run enemy state machine logic
        }
        yield return null;
    }
    IEnumerator Win()
    {
        ClickableMoveButts(false);
        _enemy.NextState();
        battleLog.text = $"{enemy.name} defeated.\nYou Win!";
        yield return null;
    }
    IEnumerator Lose()
    {
        ClickableMoveButts(false);
        battleLog.text = $"{player.name} has passed out.\nYou lose.";
        _player.Die();
        yield return null;
    }

    public void ClickableMoveButts(bool interactivity)
    {
        foreach(Button butt in _moveButts)
        {
            butt.interactable = interactivity;
        }
    }
    public void PlayerTurnOver()
    {
        ClickableMoveButts(false);
        _gameState = GameState.EnemyTurn;
        if (_enemy.monstah.shielded)
        {
            _enemy.monstah.shielded = false; //opponent's shields wear off after a turn is over
            _enemy.monstah.UpdateUI();
            battleLog.text = $"{enemy.name}'s shield has worn off.";
        }
        NextState();
    }
    public void EnemyTurnOver()
    {
        _gameState = GameState.PlayerTurn;
        if (_player.monstah.shielded)
        {
            _player.monstah.shielded = false; //opponent's shields wear off after a turn is over
            _player.monstah.UpdateUI();
            battleLog.text = $"{player.name}'s shield has worn off.";
        }
        NextState();
    }
}
