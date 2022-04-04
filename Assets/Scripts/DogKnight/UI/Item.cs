using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;

public class Item : UIBase
{
    // 1. enum �����Ӱ� ����
    enum GameObjects
    {
        BackgroundImage
    }

    private string _itemName;

    private void Start()
    {
        Init();
    }

    // 2. Item Button�� OnClick_ItemUse Bind
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        GameObject backgroundImage = GetUIComponent<GameObject>((int)GameObjects.BackgroundImage);
        backgroundImage.BindEvent(OnClick_ItemUse);
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
        Debug.Log("Clicked!");
        ItemProperty itemProperty = ItemProperty.GetItemProperty(_itemName);
        if(itemProperty.ItemNumber > 0)
        {
            itemProperty.ItemNumber--;
        }
        Destroy(this.gameObject);
        ItemAction();
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
        Character player = GameManager.Instance().GetCharacter("Player");
        ItemProperty itemProperty = ItemProperty.GetItemProperty(_itemName);

        switch (itemProperty.ItemName)
        {
            case "FlameItem":
                player._myDamage += 10;
                break;
            case "HealItem":
                if(player._myHp < player._myHpMax)
                {
                    player._myHp = player._myHp + 10 > player._myHpMax ? player._myHpMax : player._myHp + 10;
                }
                GameObject sceneUI = GameObject.Find("SceneUI");
                sceneUI.GetComponent<SceneUI>().CharacterHp();
                break;
        }
    }

    // 5. SetInfo: itemName�� _itemName�� �Ҵ�
    public void SetInfo(string itemName)
    {
        _itemName = itemName;
    }
}