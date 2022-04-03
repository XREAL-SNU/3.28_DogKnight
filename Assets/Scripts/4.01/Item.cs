using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;

public class Item : UIBase
{
    // 1. enum �����Ӱ� ����

    enum Images
    {
        Image
    }

    private string _itemName;

    private void Start()
    {
        Init();
    }

    SceneUI su;
    // 2. Item Button�� OnClick_ItemUse Bind
    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Image image = GetUIComponent<Image>((int)Images.Image);
        image.gameObject.BindEvent(OnClick_ItemUse);

        su = GameObject.Find("SceneUI").GetComponent<SceneUI>();
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
        ItemProperty item = ItemProperty.GetItemProperty(_itemName);
        if (item.ItemNumber > 0)
        {
            item.ItemNumber--;
            Destroy(this.gameObject);
            ItemAction();
        }    
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
        ItemProperty item = ItemProperty.GetItemProperty(_itemName);
        switch (item.ItemName)
        {
            //�̸��� ���� �ٲ���ϳ�? �ϴ� �ð� ������ Ÿ�Ժ��θ� ������
            case "FlameItem":
                Character player = GameManager.Instance().GetCharacter("Player") as Player;
                player._myDamage += 10;
                break;

            case "FireSpearItem":
                Character player3 = GameManager.Instance().GetCharacter("Player") as Player;
                player3.isDoubleAttack = true;
                break;

            default:
                Character player2 = GameManager.Instance().GetCharacter("Player") as Player;
                player2._myHp += 10;
                su.CharacterHp();
                break;
        }
    }

    // 5. SetInfo: itemName�� _itemName�� �Ҵ�
    public void SetInfo(string itemName)
    {
        _itemName = itemName;
    }
}