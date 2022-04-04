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
        InventoryButton,
        AttackButton,
        PlayerHpBar,
        EnemyHpBar,
        GameOver


    }


    // ������Ʈ���� �Ѱܹ��� ������
    private bool _isEnd;
    private int _gameRound;
    private string _whoseTurn;
    private Character _player;
    private Character _enemy;


    private Image hpbar_player;
    private Text gameRound_txt;

    // Attack ��ư ���� Ŭ�� ���� bool ����
    private bool _isClicked = false;


    private void Start()
    {
        Init();
        _player = GameManager.Instance().GetCharacter("Player");
        _enemy = GameManager.Instance().GetCharacter("Enemy");
        // 1. ������ ���: AddUI(this);
        GameManager.Instance().AddUI(this);
        // 1. Game Ending ���� �� �ߴ� UI ��Ȱ��ȭ

       GameObject gameEnd = GetUIComponent<GameObject>((int)GameObjects.GameOver);
       gameEnd.SetActive(false);


    }

    /// <summary>
    /// 2. Init: �� ��ҿ� �Լ� ���ε�
    /// 1) Attack Button�� OnClick_AttackButton Bind
    /// 2) �κ��丮 â ���� ��ư�� OnClick_InventoryButton Bind
    /// </summary>
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        

        GameObject inventoryButton = GetUIComponent<GameObject>((int)GameObjects.InventoryButton);
        inventoryButton.BindEvent(OnClick_InventoryButton);

        GameObject attackButton = GetUIComponent<GameObject>((int)GameObjects.AttackButton);
        attackButton.BindEvent(OnClick_AttackButton);




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
        if (!_isClicked)
        {
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
        if(_whoseTurn == "Player")
        {
            UIManager.UI.ShowPopupUI<UIPopup>("InventoryUI");
        }
    }

    // 5. GameRoundText: GameRound ���� UI�� text ������Ʈ
    public void GameRoundText()
    {
        Text _gameRoundText = UIUtils.FindUIChild<Text>(gameObject, "GameRound_text", true);
        _gameRoundText.text = $"GameRound {_gameRound}";

    }

    // 6. CharacterHp: CharacterHp UI ������Ʈ -> fillAmount �� �̿�
    public void CharacterHp()
    {
        float _playerHp = _player._myHp / _player._myHpMax;
        float _enemyHp = _enemy._myHp / _enemy._myHpMax;
        Image _playerHpBar = UIUtils.FindUIChild<Image>(gameObject, "PlayerHP", true);
        _playerHpBar.fillAmount = _playerHp;
        Image _enemyHpBar = UIUtils.FindUIChild<Image>(gameObject, "EnemyHP", true);
        _enemyHpBar.fillAmount = _enemyHp;

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
            GameObject gameEnd = GetUIComponent<GameObject>((int)GameObjects.GameOver);
            gameEnd.SetActive(true);
            Text _gameOverText = UIUtils.FindUIChild<Text>(gameObject, "GameOverText", true);
            _gameOverText.text = $"GameOver!!\n{_whoseTurn} Win!!";
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
        // 7. �ٽ� ��ư ���� �� �ֵ��� _isClicked ����

        _isClicked = false;
    }

    // 8. UIUpdate: ������Ʈ ��������Ʈ�� ��ϵ� ������ ������Ʈ �Լ� -> ���� ������Ʈ
    public void UIUpdate(int round, string turn, bool isFinish)
    {
        _isEnd = isFinish;
        _gameRound = round;
        _whoseTurn = turn;

    }
}
