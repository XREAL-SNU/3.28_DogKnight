using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;

public class SceneUI : UIScene
{
    // component 종류에 따라 분류하여 enum 구성
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
    enum Images
    {
        PlayerHpBar,
        EnemyHpBar,
        GameOverPanel,
        HealImage
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

        // GameManager의 옵저버로 등록
        GameManager.Instance().AddUI(this);

        // Start 시 뜨면 안 되는 UI 비활성화
        GetImage((int)Images.GameOverPanel).gameObject.SetActive(false);
    }

    public override void Init()
    {
        base.Init();

        // 각 요소에 함수 바인딩
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        // Attack Button에 OnClick_AttackButton Bind
        GetButton((int)Buttons.AttackButton).gameObject.BindEvent(OnClick_AttackButton);

        // 인벤토리 창 여는 버튼에 OnClick_InventoryButton Bind
        GetButton((int)Buttons.InventoryButton).gameObject.BindEvent(OnClick_InventoryButton);
    }

    public void OnClick_AttackButton(PointerEventData data)
    {
        // 애니메이션 실행 중이 아니라면,
        if (!_isClicked)
        {
            _isClicked = true;

            // 서브젝트에게 RoundNotify -> 옵저버들에게 이벤트 발행
            GameManager.Instance().RoundNotify();

            // Update UI: Text of Game Round
            GameRoundText();

            _player.Attack();
            _enemy.Attack();

            StartCoroutine(GetDamageCoroutine());
        }
    }

    public void OnClick_InventoryButton(PointerEventData data)
    {
        // Player 턴이라면,
        if (_whoseTurn.Equals("Player"))
        {
            // Inventory UIPopup 띄우기
            UIManager.UI.ShowPopupUI<UIPopup>("Inventory");
        }
    }

    // update UI: text of new game round
    public void GameRoundText()
    {
        GetText((int)Texts.GameRoundText).text = "Game Round " + _gameRound;
    }

    // Update UI: HP bar of Character
    public void CharacterHp()
    {
        GetImage((int)Images.PlayerHpBar).fillAmount = _player._myHp / _player._myHpMax;
        GetImage((int)Images.EnemyHpBar).fillAmount = _enemy._myHp / _enemy._myHpMax;
    }

    public void GameEnd()
    {
        // 게임이 끝났다면,
        if (_isEnd)
        {
            // activate UI: game over image
            GetImage((int)Images.GameOverPanel).gameObject.SetActive(true);

            // update UI: text including winner's name
            GetText((int)Texts.GameOverText).text = "Game Over!!\n" + _whoseTurn + " Win!!";
        }
    }

    // 7. GetDamageCoroutine: 각 캐릭터들의 공격/피격 애니메이션에 맞추어 UI 표현이 자연스러울 수 있도록
    IEnumerator GetDamageCoroutine()
    {
        GetButton((int)Buttons.AttackButton).interactable = false;
        if (_whoseTurn.Equals("Player"))
        {
            GetButton((int)Buttons.InventoryButton).interactable = false;
        }
        yield return new WaitForSeconds(1.2f);
        CharacterHp();

        float beforeHp = _enemy._myHp;
        yield return new WaitForSeconds(1.2f);
        GameEnd();
        float afterHp = _enemy._myHp;
        CharacterHp();
        if (beforeHp != afterHp)
        {
            GetImage((int)Images.HealImage).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.65f);
            GetImage((int)Images.HealImage).gameObject.SetActive(false);
        }
        GetButton((int)Buttons.AttackButton).interactable = true;
        if (_whoseTurn.Equals("Enemy")) GetButton((int)Buttons.InventoryButton).interactable = true;
        _isClicked = false;
    }

    // 8. UIUpdate: 서브젝트 델리게이트에 등록될 옵저버 업데이트 함수 -> 변수 업데이트
    public void UIUpdate(int round, string turn, bool isFinish)
    {
        this._gameRound = round;
        this._whoseTurn = turn;
        this._isEnd = isFinish;
    }
}