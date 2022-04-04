using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, Subject
{
    // 1. Singleton Pattern: Instance() method
    
    private static GameManager _instance;
    public static GameManager Instance()
    {
        if (_instance ==null)
        {
            _instance = FindObjectOfType<GameManager>();

            if (_instance == null)
            {
                GameObject container = new GameObject("GameManager");
                _instance = container.AddComponent<GameManager>();

            }


        }

        return _instance;

    }



    // 초기화 설정 바꾸지 말 것
    private int _gameRound = 0;
    private string _whoseTurn = "Enemy";
    private bool _isEnd = false;

    // delegate: TurnHandler, FinishHandler 선언

    public delegate void TurnHandler(int round, string turn);
    public delegate void FinishHandler(bool isFinished);

    TurnHandler _turnHandler;
    FinishHandler _finishHandler;

    /// <summary>
    /// 2. RoundNotify:
    /// 1) 현재 턴이 Enemy이면 다음 gameRound로
    ///  + Debug.Log($"GameManager: Round {gameRound}.");
    /// 2) TurnNotify() 호출
    /// </summary>
    public void RoundNotify()
    {
        if (_whoseTurn == "Enemy") 
        {
            //Enemy일때 ++되므로 처음에 ++되고 1라운드 찍고 시작
            _gameRound++;
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
    /// 

    public Image blocker;


    public void TurnNotify()
    {
        blocker.enabled = true;

        if (_whoseTurn=="Enemy")
        {   //처음에 Enemy니까 Player로 바뀌고 시작: 처음 찍히는 로그가 " Player turn"임
            _whoseTurn = "Player";
        }

        else if (_whoseTurn == "Player")
        {
            _whoseTurn = "Enemy";
        }
        Debug.Log($"GameManager: {_whoseTurn} turn.");

        _turnHandler(_gameRound, _whoseTurn);
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

    //Concrete subject의 AddObserver하는 과정이었다!!!!!!!!!!!!!!ㅁㅁㅊㅁㅊㅁㅊㅁㅊ
    //그니까 Concrete Subject에서는 그냥 Add하는 함수만 선언해놓고,
    //직접 Add하는건 "Subject가 하는게 아니라"
    //등록되고싶은 Concrete Observer가 직접
    //이 ConcreteSubject참조해서 Add하는 함수(여기서는 AddCharacter) 호출하는것
    //그렇기 때문에 Observer pattern의 장점: Subject 바꿀 필요 없이 concreteObserver붙어있는 prefab을 Instantiate하기만 하면 Awake에서 알아서 등록됨
    //그니까 각 Player 혹은 Enemy의 어떤 함수를 연결하고 싶은데, 그걸 뭉뚱거려서 Character로 GameManager에서 관리 가능
    { 

        //원래는 ovserver List에다가 append하고, Notify에서 List 다 돌면서 모든 concreteobserver들의 특정 함수 실행하는건데
        //delegate를 사용하면
        //연결된 그 concreteObserver에서 특정 함수를 바로 호출할 수 있음


        //그래서 이거는 이제 연결된 concreteObserver에 어떤 함수를 호출할건지 설정하는것!!
        _turnHandler += character.TurnUpdate;
        //그니까 TurnNofity가 실행되면 연결된 "모든" concreteobserver의 turnupdate가 실행됨


        _finishHandler += character.FinishUpdate;


    }


  
}
