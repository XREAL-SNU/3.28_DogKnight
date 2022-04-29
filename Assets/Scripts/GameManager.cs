using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, Subject
{
    private static GameManager _instance;

    public static GameManager Instance()
    {
        return _instance;
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (this != _instance)
        {
            Destroy(this.gameObject);
        }

        _instance = this;
    }

    private int _gameRound = 0;
    private string _whoseTurn = "Enemy";
    private bool _isEnd = false;

    private Dictionary<string, Character> _characterList = new();

    private delegate void TurnHandler(int round, string turn);
    TurnHandler _turnHandler;
    private delegate void FinishHandler(bool isFinish);
    FinishHandler _finishHandler;

    private delegate void UIHandler(int round, string turn, bool isFinish);
    private UIHandler _uiHandler;

    public void RoundNotify()
    {
        if (_whoseTurn == "Enemy")
        {
            _gameRound ++;
            Debug.Log($"GameManager: Round {_gameRound}.");
        }

        TurnNotify();
    }

    public void TurnNotify()
    {
        _whoseTurn = (_whoseTurn == "Enemy") ? "Player" : "Enemy";
        Debug.Log($"GameManager: {_whoseTurn} turn.");
        _turnHandler(_gameRound, _whoseTurn);
        _uiHandler(_gameRound, _whoseTurn, _isEnd);
    }

    public void EndNotify()
    {
        _isEnd = true;
        _finishHandler(_isEnd);
        _uiHandler(_gameRound, _whoseTurn, _isEnd);
        Debug.Log("GameManager: The End");
        Debug.Log($"GameManager: {_whoseTurn} is Win!");
    }

    public void AddCharacter(Character character)
    {
        _turnHandler += new TurnHandler(character.TurnUpdate);
        _finishHandler += new FinishHandler(character.FinishUpdate);
        _characterList[character._myName] = character;
    }

    public void AddUI(SceneUI ui)
    {
        _uiHandler += new UIHandler(ui.UIUpdate);
    }

    public Character GetCharacter(string name)
    {
        return _characterList.ContainsKey(name) ? _characterList[name] : null;
    }
}