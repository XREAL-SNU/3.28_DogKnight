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
        GameOverPanel
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
        GameManager.Instance().AddUI(this);
        GameObject gameOverPanel = GetUIComponent<GameObject>((int)GameObjects.GameOverPanel);
        gameOverPanel.SetActive(false);

        // 1. ������ ���: AddUI(this);
        // 1. Game Ending ���� �� �ߴ� UI ��Ȱ��ȭ
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

        GameObject AttackButton = GetUIComponent<GameObject>((int)GameObjects.AttackButton);
        GameObject InventoryButton = GetUIComponent<GameObject>((int)GameObjects.InventoryButton);

        AttackButton.BindEvent(OnClick_AttackButton);
        InventoryButton.BindEvent(OnClick_InventoryButton);

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
            _isClicked = true;
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
        if(_whoseTurn == "Enemy")
        {
            UIManager.UI.ShowPopupUI<UIPopup>("Inventory");
        }
    }

    // 5. GameRoundText: GameRound ���� UI�� text ������Ʈ
    public void GameRoundText()
    {
        Text textgameRound = UIUtils.FindUIChild<Text>(gameObject, "GameRoundText", true);
        textgameRound.text = $"Round {_gameRound}";
    }

    // 6. CharacterHp: CharacterHp UI ������Ʈ -> fillAmount �� �̿� 
    public void CharacterHp()
    {
        float _playerHpPercentage = _player._myHp / _player._myHpMax;
        float _enemyHpPercentage = _enemy._myHp / _enemy._myHpMax;

        Slider _playerHpSlider = UIUtils.FindUIChild<Slider>(gameObject, "MyHp", true);
        Slider _enemyHpSlider = UIUtils.FindUIChild<Slider>(gameObject, "EnemyHp", true);

        _playerHpSlider.value = _playerHpPercentage;
        _enemyHpSlider.value = _enemyHpPercentage;

    }

    /// <summary>
    /// 7. GameEnd:
    /// 1) ������ �����ٸ�,
    /// 2) GameEnd UI Ȱ��ȭ
    /// 3) �̱� ĳ���� �̸� Text ������Ʈ
    /// </summary>
    public void GameEnd()
    {
        if(_isEnd)
        {
            GameObject gameOverPanel = GetUIComponent<GameObject>((int)GameObjects.GameOverPanel);
            gameOverPanel.SetActive(true);
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
        this._gameRound = round;
        this._whoseTurn = turn;
        this._isEnd = isFinish;
    }
}