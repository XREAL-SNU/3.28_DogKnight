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
    private string _whoseTurn = "Enemy";
    private bool _isEnd = false;

<<<<<<< HEAD
    // 1. SceneUI�� GameManager ���� �� �� �ֵ��� ĳ���� ��ųʸ� ����
    private Dictionary<string, Character> _characterList = new Dictionary<string, Character>();

    private delegate void TurnHandler(int round, string turn);
    private TurnHandler _turnHandler;
    private delegate void FinishHandler(bool isFinish);
    private FinishHandler _finishHandler;
    // 2. UIHandler ���� (�̹����� round, turn, isFinish ��� �޴´�)
    private delegate void UIHandler(int round, string turn, bool isFinish);
    private UIHandler _uiHandler;
=======
    //List<Observer> _observers = new List<Observer>();

    // delegate: TurnHandler, FinishHandler ����
    delegate void TurnHandler();
    delegate void FinishHandler();

    TurnHandler _turnHandler;
    FinishHandler _finishHandler;
>>>>>>> 067d66f7be68ffe936c82a1071b28b66ead05353

    public void RoundNotify()
    {
<<<<<<< HEAD
        if (!_isEnd)
        {
            if (_whoseTurn == "Enemy")
            {
                _gameRound++;
                Debug.Log($"GameManager: Round {_gameRound}.");
            }
            TurnNotify();
        }
=======
        if(_whoseTurn == "Enemy")
        {
            _gameRound++;
        }
        Debug.Log($"GameManager: Round {_gameRound}");
        TurnNotify();
>>>>>>> 067d66f7be68ffe936c82a1071b28b66ead05353
    }

    public void TurnNotify()
    {
<<<<<<< HEAD
        _whoseTurn = _whoseTurn == "Enemy" ? "Player" : "Enemy";
        Debug.Log($"GameManager: {_whoseTurn} turn.");
        _turnHandler(_gameRound, _whoseTurn);
        // 2. _uiHandler ȣ��
        _uiHandler(_gameRound, _whoseTurn, _isEnd);
=======
        if(_whoseTurn=="Enemy")
        {
            _whoseTurn = "Player";
        }
        else
        {
            _whoseTurn = "Enemy";
        }
        Debug.Log($"GameManager : {_whoseTurn} turn.");
>>>>>>> 067d66f7be68ffe936c82a1071b28b66ead05353
    }

    public void EndNotify()
    {
        _isEnd = true;
<<<<<<< HEAD
        _finishHandler(_isEnd);
        // 2. _uiHandler ȣ��
        _uiHandler(_gameRound, _whoseTurn, _isEnd);
        Debug.Log("GameManager: The End");
        Debug.Log($"GameManager: {_whoseTurn} is Win!");
=======
        Debug.Log("GameManager: The End");
        Debug.Log($"GameManager: {_whoseTurn} is Win!");
        _finishHandler();
>>>>>>> 067d66f7be68ffe936c82a1071b28b66ead05353
    }

    public void AddCharacter(Character character)
    {
        _turnHandler += new TurnHandler(character.TurnUpdate);
        _finishHandler += new FinishHandler(character.FinishUpdate);
        // 1. _characterList�� �߰�
        _characterList.Add(character._myName, character);
    }

<<<<<<< HEAD
    // 3. AddUI: SceneUI �������� ���
    public void AddUI(SceneUI ui)
    {
        _uiHandler -= ui.UIUpdate;
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
        {
            Character _character;
            _characterList.TryGetValue(name, out _character);
            return _character;
        }
        return null;
=======

>>>>>>> 067d66f7be68ffe936c82a1071b28b66ead05353
    }
}