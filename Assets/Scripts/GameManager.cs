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

    // �ʱ�ȭ ���� �ٲ��� �� ��
    public int _gameRound = 0;
    public string _whoseTurn = "Enemy"; //team turn (Player or Enemy)
    public bool _isEnd = false;

    // delegate: TurnHandler, FinishHandler ����
    public delegate void TurnHandler(int round, string turn, Character character);
    public delegate void FinishHandler(bool end);
    public TurnHandler TurnEvent;
    public FinishHandler FinishEvent;

    public List<Character> players = new List<Character>();
    public List<Character> enemies = new List<Character>();
    public int turn = 0; //member turn

    /// <summary>
    /// 2. RoundNotify:
    /// 1) ���� ���� Enemy�̸� ���� gameRound��
    ///  + Debug.Log($"GameManager: Round {gameRound}.");
    /// 2) TurnNotify() ȣ��
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
    /// 2) _turnHandler ȣ��
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
    /// 2) _finishHandler ȣ��
    /// </summary>
    public void EndNotify()
    {
        _isEnd = true;
        Debug.Log("GameManager: The End");
        Debug.Log($"GameManager: {_whoseTurn} team won!");

        FinishEvent(_isEnd);
    }

    // 5. AddCharacter: _turnHandler, _finishHandler ������ �޼ҵ� �߰�
    public void AddCharacter(Character character, bool playerTeam)
    {
        TurnEvent += character.TurnUpdate;
        FinishEvent += character.FinishUpdate;
        if(playerTeam) players.Add(character);
        else enemies.Add(character);
    }

    // enum���� ����ġ��� ���� ������ �ٽ�����.
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

    public Character NextCharacter() {
        if(turn >= TeamMember(PlayerTurn()).Count - 1) return TeamMember(!PlayerTurn())[0];
        return TeamMember(PlayerTurn())[turn + 1];
    }

    public List<Character> TeamMember(bool player) {
        return player ? players : enemies;
    }
}
