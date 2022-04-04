using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, Subject
{
    // 1. Singleton Pattern: Instance() method
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

    // ??? ?? ??? ? ?
    private int _gameRound = 0;
    private string _whoseTurn = "Enemy";
    private bool _isEnd = false;

    // 1. SceneUI가 GameManager 접근 할 수 있도록 캐릭터 딕셔너리 선언
    private Dictionary<string, Character> _characterList = new();

    // delegate: TurnHandler, FinishHandler ??
    private delegate void TurnHandler(int round, string turn);
    TurnHandler _turnHandler;
    private delegate void FinishHandler(bool isFinish);
    FinishHandler _finishHandler;

    // 2. UIHandler 선언 (이번에는 round, turn, isFinish 모두 받는다)
    private delegate void UIHandler(int round, string turn, bool isFinish);
    private UIHandler _uiHandler;

    /// <summary>
    /// 2. RoundNotify:
    /// 1) ?? ?? Enemy?? ?? gameRound?
    ///  + Debug.Log($"GameManager: Round {gameRound}.");
    /// 2) TurnNotify() ??
    /// </summary>
    public void RoundNotify()
    {
        if (_whoseTurn == "Enemy")
        {
            _gameRound ++;
            Debug.Log($"GameManager: Round {_gameRound}.");
        }

        TurnNotify();
    }

    /// <summary>
    /// 3. TurnNotify:
    /// 1) whoseTurn update
    ///  + Debug.Log($"GameManager: {_whoseTurn} turn.");
    /// 2) _turnHandler ??
    /// </summary>
    public void TurnNotify()
    {
        _whoseTurn = (_whoseTurn == "Enemy") ? "Player" : "Enemy";
        Debug.Log($"GameManager: {_whoseTurn} turn.");
        _turnHandler(_gameRound, _whoseTurn);
        _uiHandler(_gameRound, _whoseTurn, _isEnd);
    }

    /// <summary>
    /// 4. EndNotify: 
    /// 1) isEnd update
    ///  + Debug.Log("GameManager: The End");
    ///  + Debug.Log($"GameManager: {_whoseTurn} is Win!");
    /// 2) _finishHandler ??
    /// </summary>
    public void EndNotify()
    {
        _isEnd = true;
        _finishHandler(_isEnd);
        _uiHandler(_gameRound, _whoseTurn, _isEnd);
        Debug.Log("GameManager: The End");
        Debug.Log($"GameManager: {_whoseTurn} is Win!");
    }

    // 5. AddCharacter: _turnHandler, _finishHandler ??? ??? ??
    public void AddCharacter(Character character)
    {
        _turnHandler += new TurnHandler(character.TurnUpdate);
        _finishHandler += new FinishHandler(character.FinishUpdate);
        _characterList[character._myName] = character;
    }

    // 3. AddUI: SceneUI 옵저버로 등록
    public void AddUI(SceneUI ui)
    {
        _uiHandler += new UIHandler(ui.UIUpdate);
    }

    /// <summary>
    /// 4. GetChracter: 넘겨 받은 name의 Character가 있다면 해당 캐릭터 반환
    /// 1) _characterList 순회하며
    /// 2) if 문과 ContainsKey(name) 이용
    /// 3) 없다면 null 반환
    /// </summary>
    public Character GetCharacter(string name)
    {
        return _characterList.ContainsKey(name) ? _characterList[name] : null;
    }
}