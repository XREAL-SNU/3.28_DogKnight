using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, Subject
{
    // 1. Singleton Pattern: Instance() method

   
    private static GameManager _instance;

    // 초기화 설정 바꾸지 말 것
    private int _gameRound = 0;
    private string _whoseTurn = "Enemy";
    private bool _isEnd = false;

    // delegate: TurnHandler, FinishHandler 선언

    delegate void TurnHandler(int round, string turn);//public void TurnUpdate(int round, string turn)-캐릭터
    delegate void FinishHandler(bool isFinish);//public void FinishUpdate(bool isFinish)-캐릭터에서
    TurnHandler _turnHandler;
    FinishHandler _finishHandler;


  
    public GameObject enemey;
    public GameObject player;
    void Start()
    {
        _turnHandler += new TurnHandler(player.GetComponent<Player>().TurnUpdate);
        _turnHandler += new TurnHandler(enemey.GetComponent<Enemy>().TurnUpdate);

        _finishHandler += new FinishHandler(player.GetComponent<Player>().FinishUpdate);
        _finishHandler += new FinishHandler(enemey.GetComponent<Enemy>().FinishUpdate);


    }

    /// <summary>
    /// 2. RoundNotify:///-완료!!!!!!!!!!!!!
    /// 1) 현재 턴이 Enemy이면 다음 gameRound로
    ///  + Debug.Log($"GameManager: Round {gameRound}.");
    /// 2) TurnNotify() 호출
    /// </summary>
    public void RoundNotify()
    {
        if (_whoseTurn == "Enemy") /// 1) 현재 턴이 Enemy이면 다음 gameRound로
        {
            _gameRound =_gameRound+1;

            Debug.Log($"GameManager: Round {_gameRound}.");///  + Debug.Log($"GameManager: Round {gameRound}.");
        }
        TurnNotify(); //whoseturn이 뭐건간에 turnnotify해야함. 
    }


/// <summary>
/// 3. TurnNotify:///-완료!!!!!!!!!!!!!
/// 1) whoseTurn update
///  + Debug.Log($"GameManager: {_whoseTurn} turn.");
/// 2) _turnHandler 호출
/// </summary>
public void TurnNotify()
    {
    if (_whoseTurn == "Enemy")  /// 1) whoseTurn update
    {
        _whoseTurn = "Player";
    }
    else if(_whoseTurn == "Player")  /// 1) whoseTurn update
    {
        _whoseTurn = "Enemy";
    }
    Debug.Log($"GameManager: {_whoseTurn} turn."); ///  + Debug.Log($"GameManager: {_whoseTurn} turn.");
    _turnHandler(_gameRound, _whoseTurn);  /// 2) _turnHandler 호출


}

/// <summary>
/// 4. EndNotify: 
/// 1) isEnd update
///  + Debug.Log("GameManager: The End");
///  + Debug.Log($"GameManager: {_whoseTurn} is Win!");
/// 2) _finishHandler 호출
/// </summary>
public void EndNotify()///-완료!!!!!!!!!!!!!
{

    _isEnd = true;/// 1) isEnd update
    Debug.Log("GameManager: The End");///  + Debug.Log("GameManager: The End");
    Debug.Log($"GameManager: {_whoseTurn} is Win!");///  + Debug.Log($"GameManager: {_whoseTurn} is Win!");
    _finishHandler(_isEnd);/// 2) _finishHandler 호출

}

    // 5. AddCharacter: _turnHandler, _finishHandler 각각에 메소드 추가 ///-완료!!!!!!!!!!!!!
    public void AddCharacter(Character character)
    {
        _turnHandler = _turnHandler + new TurnHandler(character.GetComponent<Character>().TurnUpdate);



        _finishHandler = _finishHandler + new FinishHandler(character.GetComponent<Character>().FinishUpdate);
    }
}


