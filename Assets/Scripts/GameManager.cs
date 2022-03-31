using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, Subject
{
    // 1. Singleton Pattern: Instance() method
    private static GameManager _instance;

    public static GameManager Instance()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<GameManager>();

            if (_instance == null)
            {
                GameObject container = new ("Game Manager");
                _instance = container.AddComponent<GameManager>();
            }
        }
        return _instance;
    }

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
    }

    // ??? ?? ??? ? ?
    private int _gameRound = 0;
    private string _whoseTurn = "Enemy";
    private bool _isEnd = false;

    // delegate: TurnHandler, FinishHandler ??
    delegate void TurnHandler(int round, string turn);
    TurnHandler _turnHandler;
    delegate void FinishHandler(bool isFinish);
    FinishHandler _finishHandler;

    /// <summary>
    /// 2. RoundNotify:
    /// 1) ?? ?? Enemy?? ?? gameRound?
    ///  + Debug.Log($"GameManager: Round {gameRound}.");
    /// 2) TurnNotify() ??
    /// </summary>
    public void RoundNotify()
    {
        if (_whoseTurn == "Enemy")
        {
            _gameRound += 1;
            Debug.Log($"GameManager: Round {_gameRound}.");
        }

        TurnNotify();
    }

    /// <summary>
    /// 3. TurnNotify:
    /// 1) whoseTurn update
    ///  + Debug.Log($"GameManager: {_whoseTurn} turn.");
    /// 2) _turnHandler ??
    /// </summary>
    public void TurnNotify()
    {
        _whoseTurn = (_whoseTurn == "Enemy") ? "Player" : "Enemy";
        Debug.Log($"GameManager: {_whoseTurn} turn.");
        _turnHandler(_gameRound, _whoseTurn);
    }

    /// <summary>
    /// 4. EndNotify: 
    /// 1) isEnd update
    ///  + Debug.Log("GameManager: The End");
    ///  + Debug.Log($"GameManager: {_whoseTurn} is Win!");
    /// 2) _finishHandler ??
    /// </summary>
    public void EndNotify()
    {
        _isEnd = true;
        Debug.Log("GameManager: The End");
        Debug.Log($"GameManager: {_whoseTurn} is Win!");
        _finishHandler(_isEnd);
    }

    // 5. AddCharacter: _turnHandler, _finishHandler ??? ??? ??
    public void AddCharacter(Character character)
    {
        _turnHandler += new TurnHandler(character.TurnUpdate);
        _finishHandler += new FinishHandler(character.FinishUpdate);
    }
}