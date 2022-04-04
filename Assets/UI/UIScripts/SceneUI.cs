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
        //넣고 싶은 것 넣기 (기능에서 사용할 것)
        InventoryButton,
        AttackButton,

        PlayerSlider,
        EnemySlider,

        GameRound, GameRoundText,
        GameEnd
    }


    // 서브젝트에게 넘겨받을 변수들
    private bool _isEnd;
    private int _gameRound;
    private string _whoseTurn;
    private Character _player;
    private Character _enemy;

    // Attack 버튼 이중 클릭 방지 bool 변수
    private bool _isClicked = false;


    private void Start()
    {
        Init();
        _player = GameManager.Instance().GetCharacter("Player");
        _enemy = GameManager.Instance().GetCharacter("Enemy");
        // 1. 옵저버 등록: AddUI(this); 0
        // 1. Game Ending 됐을 때 뜨는 UI 비활성화
        GameManager.Instance().AddUI(this);
        GameObject gameEnd = GetUIComponent<GameObject>((int)GameObjects.GameEnd);
        gameEnd.SetActive(false);


    }

    /// <summary>
    /// 2. Init: 각 요소에 함수 바인딩
    /// 1) Attack Button에 OnClick_AttackButton Bind 0
    /// 2) 인벤토리 창 여는 버튼에 OnClick_InventoryButton Bind 0
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
    /// 1) 애니메이션 실행 중이 아니라면(!_isClicked)
    /// 2) 서브젝트에게 RoundNotify로 옵저버들에게 이벤트 발행
    /// 3) GameRoundText();
    /// 4) 플레이어 공격 (_player.Attack)
    /// 5) 적 공격 (_enemy.Attack)
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
    /// 1) Player 턴이라면
    /// 2) Inventory UIPopup 띄우기 (ShowPopupUI) 0
    /// </summary>
    public void OnClick_InventoryButton(PointerEventData data)
    {
        if (_whoseTurn == "Player")
        {
            UIManager.Instance().ShowPopupUI<UIPopup>("Inventory"); // () 자리에 string =null이라는데..: 이건 기본 설정..! 아무 말 않으면 그렇게 나온다는 거긴 해
        }
    }

    // 5. GameRoundText: GameRound 띄우는 UI의 text 업데이트 0
    public void GameRoundText()
    {
        //this.GetComponent<InputField>();
        Text gameRoundText = UIUtils.FindUIChild<Text>(gameObject, "GameRoundText", true);
        gameRoundText.text= $"GameRound{_gameRound}";
    }

    // 6. CharacterHp: CharacterHp UI 업데이트 -> fillAmount 값 이용 0
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
    /// 1) 게임이 끝났다면,
    /// 2) GameEnd UI 활성화 0
    /// 3) 이긴 캐릭터 이름 Text 업데이트 0
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

    // 7. GetDamageCoroutine: 각 캐릭터들의 공격/피격 애니메이션에 맞추어 UI 표현이 자연스러울 수 있도록
    IEnumerator GetDamageCoroutine()
    {
        yield return new WaitForSeconds(1.2f);
        CharacterHp();
        yield return new WaitForSeconds(1.2f);
        GameEnd();
        CharacterHp();
        // 7. 다시 버튼 눌릴 수 있도록 _isClicked 조절 //단순히 이렇게 하면 되나? 시간 더 코루틴 넣지 않아도? // 위가 코루틴이다.
        //아니 근데 헷갈리는 게.. 음 그니까 GameEnd가 들어가는 게 말이 되는 거야..?
        _isClicked = false;
    }

    // 8. UIUpdate: 서브젝트 델리게이트에 등록될 옵저버 업데이트 함수 -> 변수 업데이트 0
    public void UIUpdate(int round, string turn, bool isFinish)
    {
        this._gameRound = round;
        this._whoseTurn = turn;
        this._isEnd = isFinish;
    }
}