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
    private string _whoseTurn = "Player";
    private bool _isEnd = false;

    // delegate: TurnHandler, FinishHandler ����
    delegate void TurnHandler(int round, string turn); //������ �������� ���������� �˷��ִ� ���ڵ鷯
    TurnHandler _turnHandler;

    delegate void FinisHandler(bool isFinish); //�������� �˷��ִ� �ǴϽ��ڵ鷯
    FinisHandler _finisHandler;

    private delegate void UIHandler(int round, string turn, bool isFinish);
    private UIHandler _uiHandler;

    // 1. SceneUI�� GameManager ���� �� �� �ֵ��� ĳ���� ��ųʸ� ����
    private Dictionary<string, Character> _characterList = new Dictionary<string, Character>();

    public void RoundNotify()
    {
        if (_isEnd) return;
        if (_whoseTurn == "Enemy")
        {
            _gameRound++;
        }
        TurnNotify();
    }

    public void TurnNotify()
    {
        _whoseTurn = _whoseTurn == "Enemy" ? "Player" : "Enemy";
        _turnHandler(_gameRound, _whoseTurn);
        // 2. _uiHandler ȣ��
        _uiHandler(_gameRound, _whoseTurn, _isEnd);
    }

    public void EndNotify()
    {
        _isEnd = true;

        _finisHandler(true);
        // 2. _uiHandler ȣ��
        _uiHandler(_gameRound, _whoseTurn, _isEnd);
    }

    public void AddCharacter(Character character)
    {
        _turnHandler += new TurnHandler(character.TurnUpdate);
        _finisHandler += new FinisHandler(character.FinishUpdate);
        // 1. _characterList�� �߰�
        if(character is Player)
        {
            _characterList.Add("Player", character);
        }
        else
        {
            _characterList.Add("Enemy", character);
        }
        
    }

    // 3. AddUI: SceneUI �������� ���
    public void AddUI(SceneUI ui)
    {
        _uiHandler += ui.UIUpdate;
    }

    /// <summary>
    /// 4. GetChracter: �Ѱ� ���� name�� Character�� �ִٸ� �ش� ĳ���� ��ȯ
    /// 1) _characterList ��ȸ�ϸ�
    /// 2) if ���� ContainsKey(name) �̿�
    /// 3) ���ٸ� null ��ȯ
    /// </summary>
    public Character GetCharacter(string name)
    {
        //foreach?
        if (_characterList.ContainsKey(name))
        {
            return _characterList[name];
        }

        return null;
    }
}