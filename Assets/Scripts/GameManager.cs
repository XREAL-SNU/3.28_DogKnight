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

    // delegate: TurnHandler, FinishHandler 선언
    delegate void TurnHandler(int round, string turn);//우선 int로
    delegate void FinishHandler(bool isFinish);
    TurnHandler _turnHandler;
    FinishHandler _finishHandler;

    /// <summary>
    /// 2. RoundNotify:0
    /// 1) 현재 턴이 Enemy이면 다음 gameRound로0
    ///  + Debug.Log($"GameManager: Round {gameRound}.");0
    /// 2) TurnNotify() 호출 0
    /// </summary>
    public void RoundNotify()
    {
        if (_whoseTurn == "Enemy")
        {
            _gameRound++;
            Debug.Log($"GameManager: Round {_gameRound}.");
        }

        TurnNotify();
    }

    /// <summary>
    /// 3. TurnNotify:
    /// 1) whoseTurn update0
    ///  + Debug.Log($"GameManager: {_whoseTurn} turn.");0
    /// 2) _turnHandler 호출0
    /// </summary>
    public void TurnNotify()
    {
        if (_whoseTurn == "Enemy") {
            _whoseTurn = "Player";
        }
        else{
            _whoseTurn = "Enemy";
        }

        Debug.Log($"GameManager: {_whoseTurn} turn.");
        _turnHandler(_gameRound,_whoseTurn);
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
        _isEnd = !_isEnd;
        Debug.Log("GameManager: The End");
        Debug.Log($"GameManager: {_whoseTurn} is Win!");
        _finishHandler(_isEnd);
    }


    /// 5. AddCharacter: _turnHandler, _finishHandler 각각에 메소드 추가 0
    //음.. 여기에 그 observer를 등록해야 할 것 같기도?
    public void AddCharacter(Character character)
    {

        _turnHandler -= character.TurnUpdate;
        _finishHandler -= character.FinishUpdate;
        _turnHandler += new TurnHandler(character.TurnUpdate);
        _finishHandler += new FinishHandler(character.FinishUpdate);
    }
}
