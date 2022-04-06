using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;

public class SceneUI : UIScene
{
    // 1. enum 자유롭게 구성
    enum GameObjects {
        AttackButton,
        PlayerHP,
        EnemyHP,
        RoundText,
        GameEnd,
        InventoryButton
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
        // 1. 옵저버 등록: AddUI(this);
        GameManager.Instance().AddUI(this.GetComponent<SceneUI>());
        // 1. Game Ending 됐을 때 뜨는 UI 비활성화
        GameObject GameEnd = GetUIComponent<GameObject>((int)GameObjects.GameEnd);
        GameEnd.SetActive(false);
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

        GameObject AttackButton = GetUIComponent<GameObject>((int)GameObjects.AttackButton);
        AttackButton.BindEvent(OnClick_AttackButton);

        GameObject InventoryButton = GetUIComponent<GameObject>((int)GameObjects.InventoryButton);
        InventoryButton.BindEvent(OnClick_InventoryButton);
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
        if(!_isClicked) {
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
    /// 2) Inventory UIPopup 띄우기 (ShowPopupUI)
    /// </summary>
    public void OnClick_InventoryButton(PointerEventData data)
    {
        if(_whoseTurn != "Player") {
            UIManager.UI.ShowPopupUI<UIPopup>("Inventory");
        }
    }

    // 5. GameRoundText: GameRound 띄우는 UI의 text 업데이트
    public void GameRoundText()
    {
        GameObject RoundText = GetUIComponent<GameObject>((int)GameObjects.RoundText);
        Text _RoundText = RoundText.GetComponent<Text>();

        _RoundText.text = $"Round {_gameRound}";
    }

    // 6. CharacterHp: CharacterHp UI 업데이트 -> fillAmount 값 이용
    public void CharacterHp()
    {
        GameObject PlayerHP = GetUIComponent<GameObject>((int)GameObjects.PlayerHP);
        Image PlayerHPImage = PlayerHP.GetComponent<Image>();
        GameObject EnemyHP = GetUIComponent<GameObject>((int)GameObjects.EnemyHP);
        Image EnemyHPImage = EnemyHP.GetComponent<Image>();
        PlayerHPImage.fillAmount = _player._myHp/_player._myHpMax;
        EnemyHPImage.fillAmount = _enemy._myHp/_enemy._myHpMax;
    }

    /// <summary>
    /// 7. GameEnd:
    /// 1) 게임이 끝났다면,
    /// 2) GameEnd UI 활성화
    /// 3) 이긴 캐릭터 이름 Text 업데이트
    /// </summary>
    public void GameEnd()
    {
        if(_isEnd) {
            GameObject GameEnd = GetUIComponent<GameObject>((int)GameObjects.GameEnd);
            GameEnd.SetActive(true);
            Text GameEndText = UIUtils.FindUIChild<Text>(GameEnd, "Text");
            GameEndText.text = $"GameOver!!\n{_whoseTurn} Win !!";
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
        // 7. 다시 버튼 눌릴 수 있도록 _isClicked 조절
        _isClicked = false;
    }

    // 8. UIUpdate: 서브젝트 델리게이트에 등록될 옵저버 업데이트 함수 -> 변수 업데이트
    public void UIUpdate(int round, string turn, bool isFinish)
    {
        _isEnd = isFinish;
        _gameRound = round;
        _whoseTurn = turn;
    }
}