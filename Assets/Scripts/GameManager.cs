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

    private Dictionary<string, Character> _characterList = new Dictionary<string, Character>(); 

    // delegate: TurnHandler, FinishHandler ����
    delegate void TurnHandler(int round, string turn);
    delegate void FinishHandler(bool isFinish);
    private delegate void UIHandler(int round, string turn, bool isFinish);

    TurnHandler _turnHandler;
    FinishHandler _finishHandler;
    private UIHandler _uiHandler;

    /// <summary>
    /// 2. RoundNotify:
    /// 1) ���� ���� Enemy�̸� ���� gameRound��
    ///  + Debug.Log($"GameManager: Round {gameRound}.");
    /// 2) TurnNotify() ȣ��
    /// </summary>
    public void RoundNotify()
    {
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
        _uiHandler(_gameRound, _whoseTurn, _isEnd);
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
        _finishHandler(true);
        _uiHandler(_gameRound, _whoseTurn, _isEnd);
    }

    // 5. AddCharacter: _turnHandler, _finishHandler ������ �޼ҵ� �߰�
    public void AddCharacter(Character character)
    {
        _turnHandler += new TurnHandler(character.TurnUpdate);
        _finishHandler += new FinishHandler(character.FinishUpdate);
    }

    public void ADDUI(SceneUI ui)
    {
        _uiHandler += new UIHandler(ui.UIUpdate);
    }

    /// <summary>
    /// 4. GetChracter: �Ѱ� ���� name�� Character�� �ִٸ� �ش� ĳ���� ��ȯ
    /// 1) _characterList ��ȸ�ϸ�
    /// 2) if ���� ContainsKey(name) �̿�
    /// 3) ���ٸ� null ��ȯ
    /// </summary>
    public Character GetCharacter(string name)
    {   
        for (int i = 0; i < _characterList.Count; i++)
        {
            if(_characterList.ContainsKey(name) == _characterList[name])
            {
                return _characterList[name];
            }
            else
            {
                return null;
            }
        }
    }
}
