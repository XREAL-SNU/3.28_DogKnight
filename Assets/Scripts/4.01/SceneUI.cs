using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;

public class SceneUI : UIScene
{
    // 1. enum 자유롭게 구성
    enum GameObjects
    {
        InventoryButton,
        AttackButton
    }

    // 서브젝트에게 넘겨받을 변수들
    
    private bool _isEnd;
    private int _gameRound;
    private string _whoseTurn = "Player";
    private Character _player;
    private Character _enemy;


    // Attack 버튼 이중 클릭 방지 bool 변수
    private bool _isClicked = false;

    Text roundText;
    Image playerHpImage;
    Image enemyHpImage;
    Image endImage;
    Image healImage;
    private void Start()
    {
        Init();

        // 1. 옵저버 등록: AddUI(this);
        GameManager.Instance().AddUI(this);
        // 2. Game Ending 됐을 때 뜨는 UI 비활성화

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
        //1->2->3의 순서로 꼭해야함..?ㅠㅠㅠㅠ
        inventoryButton.BindEvent(OnClick_InventoryButton);

        GetObject((int)GameObjects.AttackButton).BindEvent(OnClick_AttackButton);

        //구간 나눵
        roundText = UI_Utils.FindUIChild<Text>(gameObject, "GameRoundText", true); //자식의자식까지 찾아봐야함
        roundText.text = "GameRound: " + _gameRound.ToString();

        //구간 나눵
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

    // 5. GameRoundText: GameRound 띄우는 UI의 text 업데이트
    public void GameRoundText()
    {
        roundText.text = "GameRound: " + _gameRound.ToString();
    }

    // 6. CharacterHp: CharacterHp UI 업데이트 -> fillAmount 값 이용
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
            gameOverText.text = "GameOver" + System.Environment.NewLine + _whoseTurn.ToString() + "승리!!";
        }
    }

    // 7. GetDamageCoroutine: 각 캐릭터들의 공격/피격 애니메이션에 맞추어 UI 표현이 자연스러울 수 있도록
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
        
        // 7. 다시 버튼 눌릴 수 있도록 _isClicked 조절
        _isClicked = false;
    }

    IEnumerator offHealImage()
    {
        yield return new WaitForSeconds(0.6f);
        healImage.gameObject.SetActive(false);
    }
    // 8. UIUpdate: 서브젝트 델리게이트에 등록될 옵저버 업데이트 함수 -> 변수 업데이트
    public void UIUpdate(int round, string turn, bool isFinish)
    {
        _isEnd = isFinish;
        _gameRound = round;
        _whoseTurn = turn;

    }

}