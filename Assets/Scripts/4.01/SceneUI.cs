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
        AttackButton
    }

    // ������Ʈ���� �Ѱܹ��� ������
    
    private bool _isEnd;
    private int _gameRound;
    private string _whoseTurn = "Player";
    private Character _player;
    private Character _enemy;


    // Attack ��ư ���� Ŭ�� ���� bool ����
    private bool _isClicked = false;

    Text roundText;
    Image playerHpImage;
    Image enemyHpImage;
    Image endImage;
    Image healImage;
    private void Start()
    {
        Init();

        // 1. ������ ���: AddUI(this);
        GameManager.Instance().AddUI(this);
        // 2. Game Ending ���� �� �ߴ� UI ��Ȱ��ȭ

        _player = GameManager.Instance().GetCharacter("Player");
        _enemy = GameManager.Instance().GetCharacter("Enemy");
    }

    public override void Init()
    {
        base.Init();

        //1
        Bind<GameObject>(typeof(GameObjects));

        //2
        GameObject inventoryButton = GetUIComponent<GameObject>((int)GameObjects.InventoryButton);

        //3 
        //1->2->3�� ������ ���ؾ���..?�ФФФ�
        inventoryButton.BindEvent(OnClick_InventoryButton);

        GetObject((int)GameObjects.AttackButton).BindEvent(OnClick_AttackButton);

        //���� ����
        roundText = UI_Utils.FindUIChild<Text>(gameObject, "GameRoundText", true); //�ڽ����ڽı��� ã�ƺ�����
        roundText.text = "GameRound: " + _gameRound.ToString();

        //���� ����
        playerHpImage = UI_Utils.FindUIChild<Image>(gameObject, "PlayerHpBar", true); 
        enemyHpImage = UI_Utils.FindUIChild<Image>(gameObject, "EnemyHpBar", true);

        endImage = UI_Utils.FindUIChild<Image>(gameObject, "GameOverPanel", true);
        endImage.gameObject.SetActive(false);

        healImage = UI_Utils.FindUIChild<Image>(gameObject, "healImage", true);
        healImage.gameObject.SetActive(false);
    }

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
    public void OnClick_InventoryButton(PointerEventData data)
    {
        print(_whoseTurn);
        if(_whoseTurn == "Player")
        {
            UIManager.UI.ShowPopupUI<UIPopup>("Inventory");
        }
       
    }

    // 5. GameRoundText: GameRound ���� UI�� text ������Ʈ
    public void GameRoundText()
    {
        roundText.text = "GameRound: " + _gameRound.ToString();
    }

    // 6. CharacterHp: CharacterHp UI ������Ʈ -> fillAmount �� �̿�
    public void CharacterHp()
    {
        playerHpImage.fillAmount = _player._myHp / 100f;
        enemyHpImage.fillAmount = _enemy._myHp / 100f;
    }

    public void GameEnd()
    {
        if (_isEnd)
        {
            endImage.gameObject.SetActive(true);
            Text gameOverText = UI_Utils.FindUIChild<Text>(gameObject, "GameOverText", true);
            gameOverText.text = "GameOver" + System.Environment.NewLine + _whoseTurn.ToString() + "�¸�!!";
        }
    }

    // 7. GetDamageCoroutine: �� ĳ���͵��� ����/�ǰ� �ִϸ��̼ǿ� ���߾� UI ǥ���� �ڿ������� �� �ֵ���
    IEnumerator GetDamageCoroutine()
    {
        
        yield return new WaitForSeconds(1.2f);
        CharacterHp();
        
        yield return new WaitForSeconds(1.2f);
        GameEnd();
        Enemy enemy = FindObjectOfType<Enemy>();
        if (enemy.isHeal)
        {
            healImage.gameObject.SetActive(true);
            StartCoroutine(offHealImage());
        }

        CharacterHp();
        
        // 7. �ٽ� ��ư ���� �� �ֵ��� _isClicked ����
        _isClicked = false;
    }

    IEnumerator offHealImage()
    {
        yield return new WaitForSeconds(0.6f);
        healImage.gameObject.SetActive(false);
    }
    // 8. UIUpdate: ������Ʈ ��������Ʈ�� ��ϵ� ������ ������Ʈ �Լ� -> ���� ������Ʈ
    public void UIUpdate(int round, string turn, bool isFinish)
    {
        _isEnd = isFinish;
        _gameRound = round;
        _whoseTurn = turn;

    }

}