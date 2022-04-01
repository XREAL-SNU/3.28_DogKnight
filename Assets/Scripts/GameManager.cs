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
                GameObject obj = new GameObject("GameManager");
                _instance = obj.AddComponent<GameManager>();
            }
        }
        return _instance;
    }

    // �ʱ�ȭ ���� �ٲ��� �� ��
    private int _gameRound = 0;
    private string _whoseTurn = "Enemy";
    private bool _isEnd = false;

    // delegate: TurnHandler, FinishHandler ����
    delegate void TurnHandler(int round, string turn); //������ �������� ���������� �˷��ִ� ���ڵ鷯
    TurnHandler _turnHandler;

    delegate void FinisHandler(bool isFinish); //�������� �˷��ִ� �ǴϽ��ڵ鷯
    FinisHandler _finisHandler;

    // 1. SceneUI�� GameManager ���� �� �� �ֵ��� ĳ���� ��ųʸ� ����
    private Dictionary<string, Character> _characterList = new Dictionary<string, Character>();

    /// <summary>
    /// 2. RoundNotify:
    /// 1) ���� ���� Enemy�̸� ���� gameRound��
    ///  + Debug.Log($"GameManager: Round {gameRound}.");
    /// 2) TurnNotify() ȣ��
    /// </summary>
    public void RoundNotify()
    {
        if (_isEnd) return;
        if (_whoseTurn == "Enemy")
        {
            Debug.Log($"GameManager: Round {_gameRound++}.");
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
        _whoseTurn = _whoseTurn == "Enemy" ? "Player" : "Enemy";

        Debug.Log($"GameManager: {_whoseTurn} turn.");

        _turnHandler(_gameRound, _whoseTurn);

       
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
        Debug.Log($"GameManager: {_whoseTurn} is Win!");
        _finisHandler(true);
    }

    // 5. AddCharacter: _turnHandler, _finishHandler ������ �޼ҵ� �߰�
    public void AddCharacter(Character character)
    {
        _turnHandler += new TurnHandler(character.TurnUpdate);
        _finisHandler += new FinisHandler(character.FinishUpdate);
    }
    public void AddUI(SceneUI ui)
    {

    }
    public Character GetCharacter(string name)
    {
        return null;
    }
}