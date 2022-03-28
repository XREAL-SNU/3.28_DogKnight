using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, Subject
{
    // 1. Singleton Pattern: Instance() method
    private static GameManager _instance;
    public static GameManager Instance() {
        if(_instance == null) _instance = FindObjectOfType<GameManager>();
        return _instance;
    }

    // 초기화 설정 바꾸지 말 것
    public int _gameRound = 0;
    public string _whoseTurn = "Enemy"; //team turn (Player or Enemy)
    public bool _isEnd = false;

    // delegate: TurnHandler, FinishHandler 선언
    public delegate void TurnHandler(int round, string turn, Character character);
    public delegate void FinishHandler(bool end);
    public TurnHandler TurnEvent;
    public FinishHandler FinishEvent;

    public List<Character> players = new List<Character>();
    public List<Character> enemies = new List<Character>();
    public int turn = 0; //member turn

    /// <summary>
    /// 2. RoundNotify:
    /// 1) 현재 턴이 Enemy이면 다음 gameRound로
    ///  + Debug.Log($"GameManager: Round {gameRound}.");
    /// 2) TurnNotify() 호출
    /// </summary>
    public void RoundNotify()
    {
        if (_isEnd) return;
        if (!PlayerTurn() && turn >= enemies.Count - 1) {
            _gameRound++;
            Debug.Log($"GameManager: Round {_gameRound}.");
        }
        
        TurnNotify();
    }

    /// <summary>
    /// 3. TurnNotify:
    /// 1) whoseTurn update
    ///  + Debug.Log($"GameManager: {_whoseTurn} turn.");
    /// 2) _turnHandler 호출
    /// </summary>
    public void TurnNotify()
    {
        turn++;
        if(turn >= TeamMember(PlayerTurn()).Count) {
            SetTurn(!PlayerTurn());
            turn = 0;
            Debug.Log($"GameManager: {_whoseTurn} team's turn.");
        }

        TurnEvent(_gameRound, _whoseTurn, CurrentCharacter());
        CurrentCharacter().Attack();
    }

    /// <summary>
    /// 4. EndNotify: 
    /// 1) isEnd update
    ///  + Debug.Log("GameManager: The End");
    ///  + Debug.Log($"GameManager: {_whoseTurn} is Win!");
    /// 2) _finishHandler 호출
    /// </summary>
    public void EndNotify()
    {
        _isEnd = true;
        Debug.Log("GameManager: The End");
        Debug.Log($"GameManager: {_whoseTurn} team won!");

        FinishEvent(_isEnd);
    }

    // 5. AddCharacter: _turnHandler, _finishHandler 각각에 메소드 추가
    public void AddCharacter(Character character, bool playerTeam)
    {
        TurnEvent += character.TurnUpdate;
        FinishEvent += character.FinishUpdate;
        if(playerTeam) players.Add(character);
        else enemies.Add(character);
    }

    // enum으로 갈아치우고 싶은 마음을 다스리며.
    public bool PlayerTurn() {
        return _whoseTurn != "Enemy";
    }

    public void SetTurn(bool player) {
        _whoseTurn = player ? "Player" : "Enemy";
    }

    public int Round() {
        return _gameRound;
    }

    public Character CurrentCharacter() {
        return PlayerTurn() ? players[turn] : enemies[turn];
    }

    public List<Character> TeamMember(bool player) {
        return player ? players : enemies;
    }
}
