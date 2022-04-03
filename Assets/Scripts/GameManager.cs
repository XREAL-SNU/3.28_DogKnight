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
        }
        return _instance;
    }

    // 초기화 설정 바꾸지 말 것
    private int _gameRound = 0;
    private string _whoseTurn = "Enemy";
    private bool _isEnd = false;

    //SceneUI가 GameManager에 접근할 수 있도록 캐릭터 딕셔너리 선언
    private Dictionary<string, Character> _characterList = new Dictionary<string, Character>();

    // delegate: TurnHandler, FinishHandler 선언
    delegate void TurnHandler(int round, string turn);
    delegate void FinishHandler(bool isFinish);
    TurnHandler _turnHandler;
    FinishHandler _finishHandler;

    //delegate: UIHandler 선언
    delegate void UIHandler(int round, string turn, bool isFinish);
    UIHandler _uiHandler;

    /// <summary>
    /// 2. RoundNotify:
    /// 1) 현재 턴이 Enemy이면 다음 gameRound로
    ///  + Debug.Log($"GameManager: Round {gameRound}.");
    /// 2) TurnNotify() 호출
    /// </summary>
    public void RoundNotify()
    {
        if(!_isEnd && _whoseTurn == "Enemy")
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
    /// 2) _turnHandler 호출
    /// </summary>
    public void TurnNotify()
    {
        if(!_isEnd)
        {
            _whoseTurn = _whoseTurn == "Enemy" ? "Player" : "Enemy";
            Debug.Log($"GameManager: {_whoseTurn} turn.");
            _turnHandler(_gameRound, _whoseTurn);
            _uiHandler(_gameRound, _whoseTurn, _isEnd);
        }
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
        Debug.Log($"GameManager: {_whoseTurn} is Win!");
        _finishHandler(_isEnd);
        _uiHandler(_gameRound, _whoseTurn, _isEnd);
    }

    // 5. AddCharacter: _turnHandler, _finishHandler 각각에 메소드 추가
    public void AddCharacter(Character character)
    {
        _turnHandler += character.TurnUpdate;
        _finishHandler += character.FinishUpdate;
        _characterList.Add(character._myName, character);
    }

    public void AddUI(SceneUI ui)
    {
        _uiHandler += ui.UIUpdate;
    }

    public Character GetCharacter(string name)
    {
        if (_characterList.ContainsKey(name))
            return _characterList[name];
        else
            return null;
    }
}
