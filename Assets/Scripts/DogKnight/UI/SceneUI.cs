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

    // 서브젝트에게 넘겨받을 변수들
    private bool _isEnd;
    private int _gameRound;
    private string _whoseTurn;
    private Character _player;
    private Character _enemy;

    // Attack 버튼 이중 클릭 방지 bool 변수
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
        // 1. 옵저버 등록: AddUI(this);
        GameManager.Instance().AddUI(this);
        // 1. Game Ending 됐을 때 뜨는 UI 비활성화
        gameOverPanel = GetUIComponent<GameObject>((int)GameObjects.GameOverPanel);
        gameOverPanel.SetActive(false);

        healImage = GetUIComponent<GameObject>((int)GameObjects.HealImage);
        healImage.SetActive(false);
    }

    /// <summary>
    /// 2. Init: 각 요소에 함수 바인딩
    /// 1) Attack Button에 OnClick_AttackButton Bind
    /// 2) 인벤토리 창 여는 버튼에 OnClick_InventoryButton Bind
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
    /// 1) 애니메이션 실행 중이 아니라면(!_isClicked)
    /// 2) 서브젝트에게 RoundNotify로 옵저버들에게 이벤트 발행
    /// 3) GameRoundText();
    /// 4) 플레이어 공격 (_player.Attack)
    /// 5) 적 공격 (_enemy.Attack)
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
    /// 1) Player 턴이라면
    /// 2) Inventory UIPopup 띄우기 (ShowPopupUI)
    /// </summary>
    public void OnClick_InventoryButton(PointerEventData data)
    {
        UIManager.UI.ShowPopupUI<UIPopup>("Inventory");
    }

    // 5. GameRoundText: GameRound 띄우는 UI의 text 업데이트
    public void GameRoundText()
    {
        GetText((int)Texts.GameRoundText).text = $"GameRound{_gameRound}";
    }

    // 6. CharacterHp: CharacterHp UI 업데이트 -> fillAmount 값 이용
    public void CharacterHp()
    {
        GetImage((int)Images.PlayerHpBar).fillAmount = _player._myHp / _player._myHpMax;
        GetImage((int)Images.EnemyHpBar).fillAmount = _enemy._myHp / _enemy._myHpMax;
    }

    /// <summary>
    /// 7. GameEnd:
    /// 1) 게임이 끝났다면,
    /// 2) GameEnd UI 활성화
    /// 3) 이긴 캐릭터 이름 Text 업데이트
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

    // 7. GetDamageCoroutine: 각 캐릭터들의 공격/피격 애니메이션에 맞추어 UI 표현이 자연스러울 수 있도록
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
        
        // 7. 다시 버튼 눌릴 수 있도록 _isClicked 조절
        _isClicked = false;
        attackButton.GetComponent<Button>().interactable = true;

        yield return new WaitForSeconds(0.3f);
        healImage.SetActive(false);
    }

    // 8. UIUpdate: 서브젝트 델리게이트에 등록될 옵저버 업데이트 함수 -> 변수 업데이트
    public void UIUpdate(int round, string turn, bool isFinish)
    {
        _gameRound = round;
        _whoseTurn = turn;
        _isEnd = isFinish;
    }
}