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
        Image
    }

    private string _itemName;
    private int flameItemNum = 10;
    private int healItemNum = 15;

    private int DAMAGE_INCREASE = 5;
    private int HP_INCREASE = 7;

    private void Start()
    {
        Init();
    }

    // 2. Item Button�� OnClick_ItemUse Bind
    public override void Init()
    {
       
        Bind<GameObject>(typeof(GameObjects));

        GameObject image = GetUIComponent<GameObject>((int)GameObjects.Image);
        image.BindEvent(OnClick_ItemUse);
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
        if(_itemName == "FlameItem")
        {
            if(flameItemNum > 0)
            {
                flameItemNum--;
                Destroy(this.gameObject);

            }
        } else
        {
            if (healItemNum > 0)
            {
                healItemNum--;
                Destroy(this.gameObject);

            }
        }
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
        if (_itemName == "FlameItem")
        {
            GameManager.Instance().GetCharacter("Player").GetComponent<Player>().AddDamage(DAMAGE_INCREASE);
        } else
        {
            GameManager.Instance().GetCharacter("Player").GetComponent<Player>().AddHpAndUpdateBar(HP_INCREASE);
           
        }
    }

    // 5. SetInfo: itemName�� _itemName�� �Ҵ�
    public void SetInfo(string itemName)
    {
        _itemName = itemName;
    }
}
