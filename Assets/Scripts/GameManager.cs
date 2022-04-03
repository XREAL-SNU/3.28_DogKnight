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

    // 초기화 설정 바꾸지 말 것
    private int _gameRound = 0;
    private string _whoseTurn = "Enemy";
    private bool _isEnd = false;

    // delegate: TurnHandler, FinishHandler 선언
    delegate void TurnHandler(int round, string turn); //누구의 차례인지 옵저버에게 알려주는 턴핸들러
    TurnHandler _turnHandler;

    delegate void FinisHandler(bool isFinish); //끝났는지 알려주는 피니쉬핸들러
    FinisHandler _finisHandler;

    private delegate void UIHandler(int round, string turn, bool isFinish);
    private UIHandler _uiHandler;

    // 1. SceneUI가 GameManager 접근 할 수 있도록 캐릭터 딕셔너리 선언
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
        // 2. _uiHandler 호출
        _uiHandler(_gameRound, _whoseTurn, _isEnd);
    }

    public void EndNotify()
    {
        _isEnd = true;
        //Debug.Log("GameManager: The End");
        //Debug.Log($"GameManager: {_whoseTurn} is Win!");
        _finisHandler(true);
        // 2. _uiHandler 호출
        _uiHandler(_gameRound, _whoseTurn, _isEnd);
    }

    public void AddCharacter(Character character)
    {
        _turnHandler += new TurnHandler(character.TurnUpdate);
        _finisHandler += new FinisHandler(character.FinishUpdate);
        // 1. _characterList에 추가
        if(character is Player)
        {
            _characterList.Add("Player", character);
        }
        else
        {
            _characterList.Add("Enemy", character);
        }
        
    }

    // 3. AddUI: SceneUI 옵저버로 등록
    public void AddUI(SceneUI ui)
    {
        _uiHandler += ui.UIUpdate;
    }

    /// <summary>
    /// 4. GetChracter: 넘겨 받은 name의 Character가 있다면 해당 캐릭터 반환
    /// 1) _characterList 순회하며
    /// 2) if 문과 ContainsKey(name) 이용
    /// 3) 없다면 null 반환
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