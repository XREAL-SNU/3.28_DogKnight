using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



    // 段奄鉢 竺舛 郊荷走 源 依
    private int _gameRound = 0;
    private string _whoseTurn = "Enemy";
    private bool _isEnd = false;

    // delegate: TurnHandler, FinishHandler 識情

    public delegate void TurnHandler(int round, string turn);
    public delegate void FinishHandler(bool isFinished);

    TurnHandler _turnHandler;
    FinishHandler _finishHandler;

    /// <summary>
    /// 2. RoundNotify:
    /// 1) 薄仙 渡戚 Enemy戚檎 陥製 gameRound稽
    ///  + Debug.Log($"GameManager: Round {gameRound}.");
    /// 2) TurnNotify() 硲窒
    /// </summary>
    public void RoundNotify()
    {
        if (_whoseTurn == "Enemy") 
        {
            //Enemy析凶 ++鞠糠稽 坦製拭 ++鞠壱 1虞錘球 啄壱 獣拙
            _gameRound++;
            Debug.Log($"GameManager: Round {_gameRound}.");
        }

        TurnNotify();
    }

    /// <summary>
    /// 3. TurnNotify:
    /// 1) whoseTurn update
    ///  + Debug.Log($"GameManager: {_whoseTurn} turn.");
    /// 2) _turnHandler 硲窒
    /// </summary>
    public void TurnNotify()
    {
        if (_whoseTurn=="Enemy")
        {   //坦製拭 Enemy艦猿 Player稽 郊餓壱 獣拙: 坦製 啄備澗 稽益亜 " Player turn"績
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
    /// 2) _finishHandler 硲窒
    /// </summary>
    public void EndNotify()
    {
        _isEnd = true;
        Debug.Log("GameManager: The End");
        Debug.Log($"GameManager: {_whoseTurn} is Win!");
        _finishHandler(_isEnd);

    }

    // 5. AddCharacter: _turnHandler, _finishHandler 唖唖拭 五社球 蓄亜
    public void AddCharacter(Character character)

    //Concrete subject税 AddObserver馬澗 引舛戚醸陥!!!!!!!!!!!!!!けけずけずけずけず
    //益艦猿 Concrete Subject拭辞澗 益撹 Add馬澗 敗呪幻 識情背兜壱,
    //送羨 Add馬澗闇 "Subject亜 馬澗惟 焼艦虞"
    //去系鞠壱粛精 Concrete Observer亜 送羨
    //戚 ConcreteSubject凧繕背辞 Add馬澗 敗呪(食奄辞澗 AddCharacter) 硲窒馬澗依
    //益係奄 凶庚拭 Observer pattern税 舌繊: Subject 郊蝦 琶推 蒸戚 concreteObserver細嬢赤澗 prefab聖 Instantiate馬奄幻 馬檎 Awake拭辞 硝焼辞 去系喫
    //益艦猿 唖 Player 箸精 Enemy税 嬢恐 敗呪研 尻衣馬壱 粛精汽, 益杏 攻琴暗形辞 Character稽 GameManager拭辞 淫軒 亜管
    { 

        //据掘澗 ovserver List拭陥亜 append馬壱, Notify拭辞 List 陥 宜檎辞 乞窮 concreteobserver級税 働舛 敗呪 叔楳馬澗闇汽
        //delegate研 紫遂馬檎
        //尻衣吉 益 concreteObserver拭辞 働舛 敗呪研 郊稽 硲窒拝 呪 赤製


        //益掘辞 戚暗澗 戚薦 尻衣吉 concreteObserver拭 嬢恐 敗呪研 硲窒拝闇走 竺舛馬澗依!!
        _turnHandler += character.TurnUpdate;
        //益艦猿 TurnNofity亜 叔楳鞠檎 尻衣吉 "乞窮" concreteobserver税 turnupdate亜 叔楳喫


        _finishHandler += character.FinishUpdate;


    }
}
