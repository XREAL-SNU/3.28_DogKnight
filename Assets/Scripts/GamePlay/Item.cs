using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;

public class Item : UIBase
{
    // 1. enum �����Ӱ� ����

    private string _itemName;

    private void Start()
    {
        Init();
    }

    // 2. Item Button�� OnClick_ItemUse Bind
    public override void Init()
    {
        
    }

    /// <summary>
    /// 3. OnClick_ItemUse
    /// 1) ItemProperty.GetItemProperty�� _itemName �̿��ؼ� ItemProperty ����
    /// 2) ���� �ش� ������ ������ 0���� ũ�ٸ�
    /// 3) ���� -1 & ��ü �ı�
    /// 4) ItemAction();
    /// </summary>
    public void OnClick_ItemUse(PointerEventData data)
    {
        
    }

    /// <summary>
    /// 4. ItemAction:
    /// 1) switch ������ itemProperty.PropertyType �μ��� �ް�
    /// 2) ItemProperty.GetItemProperty�� _itemName �̿��ؼ� ItemProperty �����ؼ�
    /// 3) Damage���, GameManager.Instance().GetCharacter("Player")�� �÷��̾� �����ؼ� ������ �߰�
    /// 4) Heal�̶�� �����ϰ� �����ؼ� ü�� �߰� + SceneUI�� CharacterHP() ȣ��
    /// </summary>
    public void ItemAction()
    {
        
    }

    // 5. SetInfo: itemName�� _itemName�� �Ҵ�
    public void SetInfo(string itemName)
    {

    }
}