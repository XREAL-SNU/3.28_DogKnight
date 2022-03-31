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

    // 초기화 설정 바꾸지 말 것
    private int _gameRound = 0;
    private string _whoseTurn = "Enemy";
    private bool _isEnd = false;

    // delegate: TurnHandler, FinishHandler 선언
    delegate void TurnHandler(int round, string turn);
    delegate void FinishHandler(bool isFinish);

    TurnHandler _turnHandler;
    FinishHandler _finishHandler;

    void Start()
    {
      
        
    }
    /// <summary>
    /// 2. RoundNotify:
    /// 1) 현재 턴이 Enemy이면 다음 gameRound로
    ///  + Debug.Log($"GameManager: Round {gameRound}.");
    /// 2) TurnNotify() 호출
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
    /// 2) _turnHandler 호출
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
    /// 2) _finishHandler 호출
    /// </summary>
    public void EndNotify()
    {
        _isEnd = true;
        Debug.Log("GameManager: The End");
        Debug.Log($"GameManager: {_whoseTurn} is Win!");
        _finishHandler(_isEnd);

    }

    // 5. AddCharacter: _turnHandler, _finishHandler 각각에 메소드 추가
    public void AddCharacter(Character character)
    {
        _turnHandler += new TurnHandler(character.GetComponent<Character>().TurnUpdate);
        _finishHandler += new FinishHandler(character.GetComponent<Character>().FinishUpdate);
    }

    // 3. AddUI: SceneUI 옵저버로 등록
    public void AddUI(SceneUI ui)
    {

    }

    /// <summary>
    /// 4. GetChracter: 넘겨 받은 name의 Character가 있다면 해당 캐릭터 반환
    /// 1) _characterList 순회하며
    /// 2) if 문과 ContainsKey(name) 이용
    /// 3) 없다면 null 반환
    /// </summary>
    public Character GetCharacter(string name)
    {
        return null;
    }
}
