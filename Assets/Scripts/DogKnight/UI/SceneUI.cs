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
        AttackButton,
        InventoryButton,
        GameOverPanel,
        HealImage
    }

    enum Images
    {
        PlayerHpBar,
        EnemyHpBar
    }

    enum Texts
    {
        GameRoundText,
        GameOverText
    }

    // ������Ʈ���� �Ѱܹ��� ������
    private bool _isEnd;
    private int _gameRound;
    private string _whoseTurn;
    private Character _player;
    private Character _enemy;

    // Attack ��ư ���� Ŭ�� ���� bool ����
    private bool _isClicked = false;

    private GameObject attackButton;
    private GameObject inventoryButton;
    private GameObject gameOverPanel;
    private GameObject healImage;


    private void Start()
    {
        Init();
        _player = GameManager.Instance().GetCharacter("Player");
        _enemy = GameManager.Instance().GetCharacter("Enemy");
        // 1. ������ ���: AddUI(this);
        GameManager.Instance().AddUI(this);
        // 1. Game Ending ���� �� �ߴ� UI ��Ȱ��ȭ
        gameOverPanel = GetUIComponent<GameObject>((int)GameObjects.GameOverPanel);
        gameOverPanel.SetActive(false);

        healImage = GetUIComponent<GameObject>((int)GameObjects.HealImage);
        healImage.SetActive(false);
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
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));
        
        attackButton = GetUIComponent<GameObject>((int)GameObjects.AttackButton);
        attackButton.BindEvent(OnClick_AttackButton);

        inventoryButton = GetUIComponent<GameObject>((int)GameObjects.InventoryButton);
        inventoryButton.BindEvent(OnClick_InventoryButton);
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
        if(!_isClicked)
        {
            _isClicked = true;
            attackButton.GetComponent<Button>().interactable = false;
            GameManager.Instance().RoundNotify();
            GameRoundText();
            if(_whoseTurn == "Player")
            {
                _player.Attack();
            }
            else
            {
                _enemy.Attack();
            }
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
        UIManager.UI.ShowPopupUI<UIPopup>("Inventory");
    }

    // 5. GameRoundText: GameRound ���� UI�� text ������Ʈ
    public void GameRoundText()
    {
        GetText((int)Texts.GameRoundText).text = $"GameRound{_gameRound}";
    }

    // 6. CharacterHp: CharacterHp UI ������Ʈ -> fillAmount �� �̿�
    public void CharacterHp()
    {
        GetImage((int)Images.PlayerHpBar).fillAmount = _player._myHp / _player._myHpMax;
        GetImage((int)Images.EnemyHpBar).fillAmount = _enemy._myHp / _enemy._myHpMax;
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
            GetText((int)Texts.GameOverText).text = $"Game Over!!\n{_whoseTurn} Win!!";
        }
    }

    // 7. GetDamageCoroutine: �� ĳ���͵��� ����/�ǰ� �ִϸ��̼ǿ� ���߾� UI ǥ���� �ڿ������� �� �ֵ���
    IEnumerator GetDamageCoroutine()
    {
        yield return new WaitForSeconds(1.2f);
        CharacterHp();
        float before = _enemy._myHp;
        
        yield return new WaitForSeconds(1.2f);
        float after = _enemy._myHp;

        bool flag = (before + 10 == after);
        if(flag)
        {
            GameObject healImage = GetUIComponent<GameObject>((int)GameObjects.HealImage);
            healImage.SetActive(true);
        }
        GameEnd();
        CharacterHp();
        
        // 7. �ٽ� ��ư ���� �� �ֵ��� _isClicked ����
        _isClicked = false;
        attackButton.GetComponent<Button>().interactable = true;

        yield return new WaitForSeconds(0.3f);
        healImage.SetActive(false);
    }

    // 8. UIUpdate: ������Ʈ ��������Ʈ�� ��ϵ� ������ ������Ʈ �Լ� -> ���� ������Ʈ
    public void UIUpdate(int round, string turn, bool isFinish)
    {
        _gameRound = round;
        _whoseTurn = turn;
        _isEnd = isFinish;
    }
}