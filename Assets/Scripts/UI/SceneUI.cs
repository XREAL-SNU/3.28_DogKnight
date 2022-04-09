using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;

public class SceneUI : UIScene
{
    // 1. enum �����Ӱ� ����

        enum Buttons
    {

        AttackButton,
        InventoryButton
    }

    enum Texts
    {
        GameRoundText,
            GameOverText
    }

    enum Bars{

        PlayerHpBar,
        EnemyHpBar

    }
    enum etcs
    {
        GameOverPanel,
        HealImage

    }



    // ������Ʈ���� �Ѱܹ��� ������
    private bool _isEnd;
    private int _gameRound;
    private string _whoseTurn;
    private Character _player;
    private Character _enemy;

    // Attack ��ư ���� Ŭ�� ���� bool ����
    private bool _isClicked = false;


    private void Start()
    {
        Init();
        _player = GameManager.Instance().GetCharacter("Player");
        _enemy = GameManager.Instance().GetCharacter("Enemy");

        // 1. ������ ���: AddUI(this);
        // 1. Game Ending ���� �� �ߴ� UI ��Ȱ��ȭ

        GetImage((int)etcs.HealImage).gameObject.SetActive(false);
        GetImage((int)etcs.GameOverPanel).gameObject.SetActive(false);



    }

    /// <summary>
    /// 2. Init: �� ��ҿ�(enum��) �Լ� ���ε�
    /// 1) Attack Button�� OnClick_AttackButton Bind
    /// 2) �κ��丮 â ���� ��ư�� OnClick_InventoryButton Bind
    /// </summary>
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Bars));
        Bind<Image>(typeof(etcs));
        GetButton((int)Buttons.AttackButton).gameObject.BindEvent(OnClick_AttackButton);
        GetButton((int)Buttons.InventoryButton).gameObject.BindEvent(OnClick_InventoryButton);
    }

    /// <summary>
    /// 3. OnClick_AttackButton
    /// 1) �ִϸ��̼� ���� ���� �ƴ϶��(!_isClicked)
    /// 2) ������Ʈ���� RoundNotify�� �������鿡�� �̺�Ʈ ����
    /// 3) GameRoundText();
    /// 4) �÷��̾� ���� (_player.Attack)
    /// 5) �� ���� (_enemy.Attack)
    /// 6) StartCoroutine(GetDamageCoroutine());
    /// </summary>
    public void OnClick_AttackButton(PointerEventData data)
    {

        if (_isClicked==false)
        {
            _isClicked = true;
            GameManager.Instance().RoundNotify();
            GameRoundText();
            _player.Attack();
            _enemy.Attack();
            StartCoroutine(GetDamageCoroutine());
        }


    }

    /// <summary>
    /// 4. OnClick_InventoryButton: 
    /// 1) Player ���̶��
    /// 2) Inventory UIPopup ���� (ShowPopupUI)
    /// </summary>
    public void OnClick_InventoryButton(PointerEventData data)
    {

        if (_whoseTurn=="Enemy")
        {
            UIManager.UI.ShowPopupUI<UIPopup>("Inventory");

        }
         else if (_whoseTurn == "Player")
        {

        }

    }

    // 5. GameRoundText: GameRound ���� UI�� text ������Ʈ
    public void GameRoundText()
    {
        GetText((int)Texts.GameRoundText).text = "GameRound" + _gameRound;
    }

    // 6. CharacterHp: CharacterHp UI ������Ʈ -> fillAmount �� �̿�
    public void CharacterHp()
    {

        GetImage((int)Bars.PlayerHpBar).fillAmount = _player._myHp / _player._myHpMax;
        GetImage((int)Bars.EnemyHpBar).fillAmount = _enemy._myHp / _enemy._myHpMax;



    }


    IEnumerator GetDamageCoroutine()
    {
        GetButton((int)Buttons.AttackButton).interactable = false;
        if (_whoseTurn.Equals("Player")) GetButton((int)Buttons.InventoryButton).interactable = false;
        yield return new WaitForSeconds(1.2f);
        CharacterHp();
        float beforeHp = _enemy._myHp;
        yield return new WaitForSeconds(1.2f);
        GameEnd();
        float afterHp = _enemy._myHp;
        CharacterHp();
        if (beforeHp != afterHp)
        {
            GetImage((int)etcs.HealImage).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.65f);
            GetImage((int)etcs.HealImage).gameObject.SetActive(false);
        }
        GetButton((int)Buttons.AttackButton).interactable = true;
        if (_whoseTurn.Equals("Enemy")) GetButton((int)Buttons.InventoryButton).interactable = true;
        _isClicked = false;
    }




    /// <summary>
    /// 7. GameEnd:
    /// 1) ������ �����ٸ�,
    /// 2) GameEnd UI Ȱ��ȭ
    /// 3) �̱� ĳ���� �̸� Text ������Ʈ
    /// </summary>
    public void GameEnd()
    {
        if (_isEnd)
        {
            GetImage((int)etcs.GameOverPanel).gameObject.SetActive(true);
            GetText((int)Texts.GameOverText).text = "Gameover!!\n" + _whoseTurn + " Win!!";
        }
    }

    // 7. GetDamageCoroutine: �� ĳ���͵��� ����/�ǰ� �ִϸ��̼ǿ� ���߾� UI ǥ���� �ڿ������� �� �ֵ���
   

    // 8. UIUpdate: ������Ʈ ��������Ʈ�� ��ϵ� ������ ������Ʈ �Լ� -> ���� ������Ʈ
    public void UIUpdate(int round, string turn, bool isFinish)
    {
        this._gameRound = round;
        this._whoseTurn = turn;
        this._isEnd = isFinish;
    }
}