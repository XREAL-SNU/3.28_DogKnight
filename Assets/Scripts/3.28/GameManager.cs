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
        return _instance;
    }

    // �ʱ�ȭ ���� �ٲ��� �� ��
    private int _gameRound = 0;
    private string _whoseTurn = "Enemy";
    private bool _isEnd = false;

    public Text gameRoundText;
    public Button attackButton;
    public GameObject Inventory;

    // delegate: TurnHandler, FinishHandler ����
    delegate void TurnHandler(int round, string turn);
    delegate void FinishHandler(bool isFinish);

    TurnHandler _turnHandler;
    FinishHandler _finishHandler;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (this != _instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private Dictionary<string, Character> _characterList = new Dictionary<string, Character>();
    // 2. UIHandler ���� (�̹����� round, turn, isFinish ��� �޴´�)
    private delegate void UIHandler(int round, string turn, bool isFinish);
    private UIHandler _uiHandler;
    private SceneUI sceneUI;

    /// <summary>
    /// 2. RoundNotify:
    /// 1) ���� ���� Enemy�̸� ���� gameRound��
    ///  + Debug.Log($"GameManager: Round {gameRound}.");
    /// 2) TurnNotify() ȣ��
    /// </summary>
    public void RoundNotify()
    {
        StartCoroutine("roundNotify");
    }

    public void showInventory()
    {
        if (_whoseTurn == "Player")
        {
            Inventory.SetActive(true);
        }
    }
    private IEnumerator roundNotify()
    {
        
        attackButton.interactable = false;
        Debug.Log("false");
      

        if (_whoseTurn == "Enemy")
        {
            _gameRound += 1;
            gameRoundText.text = "GameRound" + _gameRound;

        }
        TurnNotify();

        yield return new WaitForSeconds(2);
        
        attackButton.interactable = true;
     
    }
    /// <summary>
    /// 3. TurnNotify:
    /// 1) whoseTurn update
    ///  + Debug.Log($"GameManager: {_whoseTurn} turn.");
    /// 2) _turnHandler ȣ��
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
    {
        _turnHandler += new TurnHandler(character.GetComponent<Character>().TurnUpdate);
        _finishHandler += new FinishHandler(character.GetComponent<Character>().FinishUpdate);
        this._characterList.Add(character.name, character);
        // 1. _characterList�� �߰�
    }

    // 3. AddUI: SceneUI �������� ���
    public void AddUI(SceneUI ui)
    {
        sceneUI = ui;
        _uiHandler += ui.UIUpdate;
    }

    /// <summary>
    /// 4. GetChracter: �Ѱ� ���� name�� Character�� �ִٸ� �ش� ĳ���� ��ȯ
    /// 1) _characterList ��ȸ�ϸ�
    /// 2) if ���� ContainsKey(name) �̿�
    /// 3) ���ٸ� null ��ȯ
    /// </summary>
    public Character GetCharacter(string name)
    {
        if(this._characterList.ContainsKey(name))
        {
            return this._characterList[name];
        }
        return null;
    }
}
