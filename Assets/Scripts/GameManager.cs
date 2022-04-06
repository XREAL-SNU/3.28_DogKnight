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

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (this != _instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private int _gameRound = 0;
    private string _whoseTurn = "Player"; //ó�� turn�� Player�� �ʱ�ȭ��.
    private bool _isEnd = false;

    // 1. SceneUI�� GameManager ���� �� �� �ֵ��� ĳ���� ��ųʸ� ����
    private Dictionary<string, Character> _characterList = new Dictionary<string, Character>();

    private delegate void TurnHandler(int round, string turn);
    private TurnHandler _turnHandler;
    private delegate void FinishHandler(bool isFinish);
    private FinishHandler _finishHandler;
    // 2. UIHandler ���� (�̹����� round, turn, isFinish ��� �޴´�)
    private delegate void UIHandler(int round, string turn, bool isFinish);
    private UIHandler _uiHandler;

    public void RoundNotify()
    {
        if (!_isEnd)
        {
            if (_whoseTurn == "Player") //Player�� �ʱ�ȭ�߱� ������ Player �ϸ��� gameRound ���.
            {
                _gameRound++;
                Debug.Log($"GameManager: Round {_gameRound}.");
            }
            TurnNotify();
        }
    }

    ///AttackButton Ŭ�� �� RoundNotify -> TurnNotify ����ǹǷ� ó���� _whoseTurn ���.
    ///_whoseTurn �ٲٱ� ���� _turnHandler update�ؼ� �ùٸ� character�� �����ϵ��� ��.
    ///_uiHandler update�ϱ� ���� _whoseTurn �ٲ㼭 Enemy �Ͽ����� Inventory �˾� �� �ǰ� ��.
    public void TurnNotify()
    {
        Debug.Log($"GameManager: {_whoseTurn} turn.");
        
        _turnHandler(_gameRound, _whoseTurn);
        _whoseTurn = _whoseTurn == "Enemy" ? "Player" : "Enemy";
        // 2. _uiHandler ȣ��
        _uiHandler(_gameRound, _whoseTurn, _isEnd);
    }

    public void EndNotify()
    {
        _isEnd = true;
        _finishHandler(_isEnd);
        // 2. _uiHandler ȣ��
        _whoseTurn = _whoseTurn == "Enemy" ? "Player" : "Enemy"; //_uiHandler �� �ܼ�â ��� ���� _whoseTurn �ٲ㼭 �ùٸ��� ��µǰ� ��.
        _uiHandler(_gameRound, _whoseTurn, _isEnd);
        Debug.Log("GameManager: The End");
        Debug.Log($"GameManager: {_whoseTurn} Win!");
    }

    public void AddCharacter(Character character)
    {
        _turnHandler += new TurnHandler(character.TurnUpdate);
        _finishHandler += new FinishHandler(character.FinishUpdate);
        // 1. _characterList�� �߰�
        _characterList.Add(character._myName, character);
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
        if (_characterList.ContainsKey(name))
            return _characterList[name];
        else
            return null;
    }
}