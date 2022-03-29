using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, Subject
{
    // 1. Singleton Pattern: Instance() method
    private static GameManager _instance;

    // �ʱ�ȭ ���� �ٲ��� �� ��
    private int _gameRound = 0;
    private string _whoseTurn = "Enemy";
    private bool _isEnd = false;

    // delegate: TurnHandler, FinishHandler ����
    delegate void TurnHandler(int round, string turn);
    delegate void FinishHandler(bool isFinish);

    GameObject enemey;
    GameObject player;

    TurnHandler _turnHandler;
    FinishHandler _finishHandler;

    void Start()
    {
        Debug.Log("shown");
        _turnHandler += new TurnHandler(player.GetComponent<Player>().TurnUpdate);
        _turnHandler += new TurnHandler(enemey.GetComponent<Enemy>().TurnUpdate);

        _finishHandler += new FinishHandler(player.GetComponent<Player>().FinishUpdate);
        _finishHandler += new FinishHandler(enemey.GetComponent<Enemy>().FinishUpdate);
        
    }
    /// <summary>
    /// 2. RoundNotify:
    /// 1) ���� ���� Enemy�̸� ���� gameRound��
    ///  + Debug.Log($"GameManager: Round {gameRound}.");
    /// 2) TurnNotify() ȣ��
    /// </summary>
    public void RoundNotify()
    {
        if(_whoseTurn == "Enemy")
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
    /// 2) _turnHandler ȣ��
    /// </summary>
    public void TurnNotify()
    {
       if(_whoseTurn == "Enemy")
        {
            _whoseTurn = "Player";
        } else
        {
            _whoseTurn = "Enemy";
        }
        _turnHandler(_gameRound, _whoseTurn);
        Debug.Log($"GameManager: {_whoseTurn} turn.");

        
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

    }

    // 5. AddCharacter: _turnHandler, _finishHandler ������ �޼ҵ� �߰�
    public void AddCharacter(Character character)
    {

    }
}
