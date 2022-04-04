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



    // �ʱ�ȭ ���� �ٲ��� �� ��
    private int _gameRound = 0;
    private string _whoseTurn = "Enemy";
    private bool _isEnd = false;

    // delegate: TurnHandler, FinishHandler ����

    public delegate void TurnHandler(int round, string turn);
    public delegate void FinishHandler(bool isFinished);

    TurnHandler _turnHandler;
    FinishHandler _finishHandler;

    /// <summary>
    /// 2. RoundNotify:
    /// 1) ���� ���� Enemy�̸� ���� gameRound��
    ///  + Debug.Log($"GameManager: Round {gameRound}.");
    /// 2) TurnNotify() ȣ��
    /// </summary>
    public void RoundNotify()
    {
        if (_whoseTurn == "Enemy") 
        {
            //Enemy�϶� ++�ǹǷ� ó���� ++�ǰ� 1���� ��� ����
            _gameRound++;
            Debug.Log($"GameManager: Round {_gameRound}.");
        }

        TurnNotify();
    }

    /// <summary>
    /// 3. TurnNotify:
    /// 1) whoseTurn update
    ///  + Debug.Log($"GameManager: {_whoseTurn} turn.");
    /// 2) _turnHandler ȣ��
    /// </summary>
    /// 

    public Image blocker;


    public void TurnNotify()
    {
        blocker.enabled = true;

        if (_whoseTurn=="Enemy")
        {   //ó���� Enemy�ϱ� Player�� �ٲ�� ����: ó�� ������ �αװ� " Player turn"��
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
    /// 2) _finishHandler ȣ��
    /// </summary>
    public void EndNotify()
    {
        _isEnd = true;
        Debug.Log("GameManager: The End");
        Debug.Log($"GameManager: {_whoseTurn} is Win!");
        _finishHandler(_isEnd);

    }

    // 5. AddCharacter: _turnHandler, _finishHandler ������ �޼ҵ� �߰�
    public void AddCharacter(Character character)

    //Concrete subject�� AddObserver�ϴ� �����̾���!!!!!!!!!!!!!!������������������
    //�״ϱ� Concrete Subject������ �׳� Add�ϴ� �Լ��� �����س���,
    //���� Add�ϴ°� "Subject�� �ϴ°� �ƴ϶�"
    //��ϵǰ���� Concrete Observer�� ����
    //�� ConcreteSubject�����ؼ� Add�ϴ� �Լ�(���⼭�� AddCharacter) ȣ���ϴ°�
    //�׷��� ������ Observer pattern�� ����: Subject �ٲ� �ʿ� ���� concreteObserver�پ��ִ� prefab�� Instantiate�ϱ⸸ �ϸ� Awake���� �˾Ƽ� ��ϵ�
    //�״ϱ� �� Player Ȥ�� Enemy�� � �Լ��� �����ϰ� ������, �װ� ���װŷ��� Character�� GameManager���� ���� ����
    { 

        //������ ovserver List���ٰ� append�ϰ�, Notify���� List �� ���鼭 ��� concreteobserver���� Ư�� �Լ� �����ϴ°ǵ�
        //delegate�� ����ϸ�
        //����� �� concreteObserver���� Ư�� �Լ��� �ٷ� ȣ���� �� ����


        //�׷��� �̰Ŵ� ���� ����� concreteObserver�� � �Լ��� ȣ���Ұ��� �����ϴ°�!!
        _turnHandler += character.TurnUpdate;
        //�״ϱ� TurnNofity�� ����Ǹ� ����� "���" concreteobserver�� turnupdate�� �����


        _finishHandler += character.FinishUpdate;


    }


  
}
