using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;

public class SceneUI : UIScene
{
    // 1. enum �����Ӱ� ����

    enum GameObjects
    {
        //�ְ� ���� �� �ֱ� (��ɿ��� ����� ��)
        InventoryButton,
        AttackButton,

        PlayerSlider,
        EnemySlider,

        GameRound, GameRoundText,
        GameEnd
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
        // 1. ������ ���: AddUI(this); 0
        // 1. Game Ending ���� �� �ߴ� UI ��Ȱ��ȭ
        GameManager.Instance().AddUI(this);
        GameObject gameEnd = GetUIComponent<GameObject>((int)GameObjects.GameEnd);
        gameEnd.SetActive(false);


    }

    /// <summary>
    /// 2. Init: �� ��ҿ� �Լ� ���ε�
    /// 1) Attack Button�� OnClick_AttackButton Bind 0
    /// 2) �κ��丮 â ���� ��ư�� OnClick_InventoryButton Bind 0
    /// </summary>
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject attackButton = GetUIComponent<GameObject>((int)GameObjects.AttackButton);
        attackButton.BindEvent(OnClick_AttackButton);

        GameObject inventoryButton = GetUIComponent<GameObject>((int)GameObjects.InventoryButton);
        inventoryButton.BindEvent(OnClick_InventoryButton);

    }

    /// <summary>
    /// 3. OnClick_AttackButton ============================================= 0
    /// 1) �ִϸ��̼� ���� ���� �ƴ϶��(!_isClicked)
    /// 2) ������Ʈ���� RoundNotify�� �������鿡�� �̺�Ʈ ����
    /// 3) GameRoundText();
    /// 4) �÷��̾� ���� (_player.Attack)
    /// 5) �� ���� (_enemy.Attack)
    /// 6) StartCoroutine(GetDamageCoroutine());
    /// </summary>
    public void OnClick_AttackButton(PointerEventData data)
    {
        if (!_isClicked)
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
    /// 2) Inventory UIPopup ���� (ShowPopupUI) 0
    /// </summary>
    public void OnClick_InventoryButton(PointerEventData data)
    {
        if (_whoseTurn == "Enemy")
        {
            UIManager.Instance().ShowPopupUI<UIPopup>("Inventory"); // () �ڸ��� string =null�̶�µ�..: �̰� �⺻ ����..! �ƹ� �� ������ �׷��� ���´ٴ� �ű� ��
        }
    }

    // 5. GameRoundText: GameRound ���� UI�� text ������Ʈ 0
    public void GameRoundText()
    {
        //this.GetComponent<InputField>();
        Text gameRoundText = UIUtils.FindUIChild<Text>(gameObject, "GameRoundText", true);
        gameRoundText.text= $"GameRound{_gameRound}";
    }

    // 6. CharacterHp: CharacterHp UI ������Ʈ -> fillAmount �� �̿� 0
    public void CharacterHp()
    {
        Slider playerSlider = UIUtils.FindUIChild<Slider>(gameObject, "PlayerSlider", true);
        Slider enemySlider = UIUtils.FindUIChild<Slider>(gameObject, "EnemySlider", true);
        float _playerFillAmount=_player._myHp / _player._myHpMax;
        float _enemyFillAmount = _enemy._myHp / _enemy._myHpMax;
        playerSlider.value = _playerFillAmount;
        enemySlider.value = _enemyFillAmount;
    }

    /// <summary>
    /// 7. GameEnd:
    /// 1) ������ �����ٸ�,
    /// 2) GameEnd UI Ȱ��ȭ 0
    /// 3) �̱� ĳ���� �̸� Text ������Ʈ 0
    /// </summary>
    public void GameEnd()
    {
        if (_isEnd)
        {

            GameObject gameEnd = GetUIComponent<GameObject>((int)GameObjects.GameEnd); 
            gameEnd.SetActive(true);

            Text winner = UIUtils.FindUIChild<Text>(gameObject, "Winner", true);
            winner.text= $"{_whoseTurn} Wins!";
        }
    }

    // 7. GetDamageCoroutine: �� ĳ���͵��� ����/�ǰ� �ִϸ��̼ǿ� ���߾� UI ǥ���� �ڿ������� �� �ֵ���
    IEnumerator GetDamageCoroutine()
    {
        yield return new WaitForSeconds(1.2f);
        CharacterHp();
        yield return new WaitForSeconds(1.2f);
        GameEnd();
        CharacterHp();
        // 7. �ٽ� ��ư ���� �� �ֵ��� _isClicked ���� //�ܼ��� �̷��� �ϸ� �ǳ�? �ð� �� �ڷ�ƾ ���� �ʾƵ�? // ���� �ڷ�ƾ�̴�.
        //�ƴ� �ٵ� �򰥸��� ��.. �� �״ϱ� GameEnd�� ���� �� ���� �Ǵ� �ž�..?
        _isClicked = false;
    }

    // 8. UIUpdate: ������Ʈ ��������Ʈ�� ��ϵ� ������ ������Ʈ �Լ� -> ���� ������Ʈ 0
    public void UIUpdate(int round, string turn, bool isFinish)
    {
        this._gameRound = round;
        this._whoseTurn = turn;
        this._isEnd = isFinish;
    }
}