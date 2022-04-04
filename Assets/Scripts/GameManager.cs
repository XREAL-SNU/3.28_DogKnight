using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public string _whoseTurn = "Player"; //team turn (Player or Enemy)
    public bool _isEnd = false;

    // delegate: TurnHandler, FinishHandler ����
    public delegate void TurnHandler(int round, string turn, Character character);
    public delegate void FinishHandler(bool end);
    public delegate void TurnEndHandler();
    public TurnHandler TurnEvent;
    public FinishHandler FinishEvent;
    public TurnEndHandler TurnEndEvent;

    public List<Character> players = new List<Character>();
    public List<Character> enemies = new List<Character>();
    public int turn = -1; //member turn

    public List<Character> deadPlayers = new List<Character>();
    public List<Character> deadEnemies = new List<Character>();

    public GameObject gameEndDialog;
    public TextMeshProUGUI gameEndText;

    /// <summary>
    /// 2. RoundNotify:
    /// 1) ���� ���� Enemy�̸� ���� gameRound��
    ///  + Debug.Log($"GameManager: Round {gameRound}.");
    /// 2) TurnNotify() ȣ��
    /// </summary>
    public void RoundNotify(Action<Character> attack) //���̷����� ���ݰ� �� ü������ ���ս��״µ�, �̸� �и��Ϸ��� ����� ���İ�Ƽ �ڵ尡 ź��... 
    {
        if (_isEnd) return;
        if (!PlayerTurn() && turn >= enemies.Count - 1) {
            _gameRound++;
            Debug.Log($"GameManager: Round {_gameRound}.");
        }
        
        TurnNotify(attack);
    }

    public void RoundNotify() {
        RoundNotify(c => c.Attack(null));
    }

    /// <summary>
    /// 3. TurnNotify:
    /// 1) whoseTurn update
    ///  + Debug.Log($"GameManager: {_whoseTurn} turn.");
    /// 2) _turnHandler ȣ��
    /// </summary>
    public void TurnNotify(Action<Character> attack)
    {
        turn++;
        if(turn >= TeamMember(PlayerTurn()).Count) {
            SetTurn(!PlayerTurn());
            turn = 0;
            Debug.Log($"GameManager: {_whoseTurn} team's turn.");
        }

        if (CurrentCharacter().dead) {
            RoundNotify();//move to next character
        }
        else {
            TurnEvent(_gameRound, _whoseTurn, CurrentCharacter());
            attack(CurrentCharacter());
        }
    }

    public void TurnNotify() {
        TurnNotify(c => c.Attack(null));
    }

    public void TurnEnd() {
        TurnEndEvent();
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
        //first, check if each team has a survivor
        bool plw = enemies.Count <= 0, enw = players.Count <= 0;

        if (!plw && !enw) return;

        string t = plw ? "Player" : "Enemy";
        _isEnd = true;
        Debug.Log("GameManager: The End");
        if(plw && enw) {
            gameEndText.text = "Tie!";
            gameEndText.color = Color.white;
        }
        else if (plw) {
            gameEndText.text = "You win!";
        }
        else {
            gameEndText.text = "You lose...";
            gameEndText.color = Color.red;
        }

        FinishEvent(_isEnd);

        gameEndDialog.SetActive(true);
        gameEndDialog.transform.SetAsLastSibling();
    }

    public void DeadNotify(Character c) {
        if (!c.dead) c.dead = true;
        if (players.Contains(c)) {
            players.Remove(c);
            deadPlayers.Add(c);
        }
        else {
            enemies.Remove(c);
            deadEnemies.Add(c);
        }
    }

    // 5. AddCharacter: _turnHandler, _finishHandler ������ �޼ҵ� �߰�
    public void AddCharacter(Character character, bool playerTeam)
    {
        TurnEvent += character.TurnUpdate;
        FinishEvent += character.FinishUpdate;
        if(playerTeam) players.Add(character);
        else enemies.Add(character);

        UI.AddCharacterUI(character, !playerTeam);
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

    public bool NextPlayerTurn() {
        if (turn >= TeamMember(PlayerTurn()).Count - 1) {
            if (TeamMember(!PlayerTurn()).Count == 0) return PlayerTurn();
            return !PlayerTurn();
        }
        return PlayerTurn();
    }

    public Character NextCharacter() {
        if (turn >= TeamMember(PlayerTurn()).Count - 1) {
            if(TeamMember(!PlayerTurn()).Count == 0) return TeamMember(PlayerTurn())[0];
            return TeamMember(!PlayerTurn())[0];
        }
        return TeamMember(PlayerTurn())[turn + 1];
    }

    public List<Character> TeamMember(bool player) {
        return player ? players : enemies;
    }

    public List<Character> DeadMember(bool player) {
        return player ? deadPlayers : deadEnemies;
    }
}
