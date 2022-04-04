using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;

public class SceneUI : UIScene
{
    // 1. enum �����Ӱ� ����

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
        
    }

    /// <summary>
    /// 4. OnClick_InventoryButton: 
    /// 1) Player ���̶��
    /// 2) Inventory UIPopup ���� (ShowPopupUI)
    /// </summary>
    public void OnClick_InventoryButton(PointerEventData data)
    {
        
    }

    // 5. GameRoundText: GameRound ���� UI�� text ������Ʈ
    public void GameRoundText()
    {
        
    }

    // 6. CharacterHp: CharacterHp UI ������Ʈ -> fillAmount �� �̿�
    public void CharacterHp()
    {
        
    }

    /// <summary>
    /// 7. GameEnd:
    /// 1) ������ �����ٸ�,
    /// 2) GameEnd UI Ȱ��ȭ
    /// 3) �̱� ĳ���� �̸� Text ������Ʈ
    /// </summary>
    public void GameEnd()
    {
        
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
    }

    // 8. UIUpdate: ������Ʈ ��������Ʈ�� ��ϵ� ������ ������Ʈ �Լ� -> ���� ������Ʈ
    public void UIUpdate(int round, string turn, bool isFinish)
    {
        
    }
}