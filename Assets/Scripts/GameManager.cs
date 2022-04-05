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

    private void Awake()
    {
        if(_instance==null)
        {
            _instance = this; //instance할당해주기
            DontDestroyOnLoad(this.gameObject); // Scene 이 바뀌어도 유지
        }
        else
        {
            if(this != _instance)
            {
                Destroy(this.gameObject); //여러개인 경우 삭제
            }
        }
    }
    private Character[] characters;

    // 초기화 설정 바꾸지 말 것
    private int _gameRound = 0;
    private string _whoseTurn = "Enemy";
    private bool _isEnd = false;

    // delegate: TurnHandler, FinishHandler 선언
    private delegate void TurnHandler(int round, string turn);
    private TurnHandler _turnHandler;
    delegate void FinishHandler(bool isFinish);
    private FinishHandler _finishHandler;


 
    /// <summary>
    /// 2. RoundNotify:
    /// 1) 현재 턴이 Enemy이면 다음 gameRound로
    ///  + Debug.Log($"GameManager: Round {gameRound}.");
    /// 2) TurnNotify() 호출
    /// </summary>
    public void RoundNotify()
    {
        if (!_isEnd)
        {
            UIManager.Instance().InvenBtnActive(_whoseTurn);

            if (_whoseTurn == "Enemy")
            {

                _gameRound++;
                UIManager.Instance().roundText.text = "Round"+_gameRound.ToString();
                UIManager.Instance().InvenBtn.interactable = false;
            }
            else
            {
                UIManager.Instance().InvenBtn.interactable = true;
            }
            TurnNotify();

        }
    }

    /// <summary>
    /// 3. TurnNotify:
    /// 1) whoseTurn update
    ///  + Debug.Log($"GameManager: {_whoseTurn} turn.");
    /// 2) _turnHandler 호출
    /// </summary>
    public void TurnNotify()
    {
        if (_whoseTurn != "Player")
            _whoseTurn = "Player";
        else
            _whoseTurn = "Enemy";

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
        _finishHandler(_isEnd);
        UIManager.Instance().GameOverImage.SetActive(true);
        UIManager.Instance().GameOverUpdate(_whoseTurn);
         Debug.Log("GameManager: The End");
        Debug.Log($"GameManager: {_whoseTurn} is Win!");
    }

    // 5. AddCharacter: _turnHandler, _finishHandler 각각에 메소드 추가
    public void AddCharacter(Character character)
    {
        //Character Class에 Handler추가
        _turnHandler += new TurnHandler(character.TurnUpdate);
        _finishHandler += new FinishHandler(character.FinishUpdate);

    }
}
