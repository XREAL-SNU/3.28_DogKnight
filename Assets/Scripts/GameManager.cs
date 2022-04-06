using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, Subject//subject 클래스 
{
    // 1. Singleton Pattern: Instance() method
    public static GameManager Instance()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<GameManager>();

            if (_instance == null)
            {
                GameObject container = new GameObject("Game Manager");

                _instance = container.AddComponent<GameManager>();
            }
        }
        return _instance;
    }
    private static GameManager _instance;

    void Awake()
    {
        _instance = this;
    }


    // 초기화 설정 바꾸지 말 것
    private int _gameRound = 0;
    private string _whoseTurn = "Enemy";
    private bool _isEnd = false;

    // delegate: TurnHandler, FinishHandler 선언
    delegate void TurnHandler(int round, string turn);
    delegate void FinishHandler(bool isFinish);

    TurnHandler _turnHandler ; 
    FinishHandler _finishHandler;


    // 1. SceneUI가 GameManager 접근 할 수 있도록 캐릭터 딕셔너리 선언
    private Dictionary<string, Character> _characterList = new Dictionary<string, Character>();

    // 2. UIHandler 선언 (이번에는 round, turn, isFinish 모두 받는다)
    private delegate void UIHandler(int round, string turn, bool isFinish);
    private UIHandler _uiHandler;
    


    /// <summary>
    /// 2. RoundNotify:
    /// 1) 현재 턴이 Enemy이면 다음 gameRound로
    ///  + Debug.Log($"GameManager: Round {gameRound}.");
    /// 2) TurnNotify() 호출
    /// </summary>
    public void RoundNotify()
    {
        if (_isEnd) return;
        if(_whoseTurn == "Enemy")
        {
            Debug.Log($"GameManager:Round{++_gameRound}.");
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
        if(_whoseTurn == "Enemy")
        {
            _whoseTurn = "Player";
            Debug.Log($"GameManager:{_whoseTurn} turn.");
        }
        else if(_whoseTurn == "Player")
        {
            _whoseTurn = "Enemy";
            Debug.Log($"GameManager:{_whoseTurn} turn.");
        }
        _turnHandler(_gameRound,_whoseTurn);
        _uiHandler(_gameRound, _whoseTurn, _isEnd);
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
        _finishHandler(_isEnd);
        _uiHandler(_gameRound, _whoseTurn, _isEnd);
        Debug.Log("GameManager: The End");
        Debug.Log($"GameManager:{_whoseTurn} is Win!");
       

    }

    // 5. AddCharacter: _turnHandler, _finishHandler 각각에 메소드 추가
    public void AddCharacter(Character character)
    {
        _turnHandler += new TurnHandler(character.TurnUpdate);

        _finishHandler += new FinishHandler(character.FinishUpdate);
        _characterList.Add(character._myName, character);
        
        
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
        if (_characterList.ContainsKey(name))
        {
            return _characterList[name];
        }
        return null;
    }
}
