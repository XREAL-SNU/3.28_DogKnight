using System.Collections;
using System.Collections.Generic;
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

    enum Texts
    {
        Text
    }

    private string _itemName;
    private string _name;

    private void Start()
    {
        Init();
    }

    // 2. Item Button�� OnClick_ItemUse Bind
    public override void Init()
    {
        Bind<GameObject>(typeof(Images));
        GameObject image = GetUIComponent<GameObject>((int)Images.Image);
        image.BindEvent(OnClick_ItemUse);

    }

    /// <summary>
    /// 3. OnClick_ItemUse
    /// 1) 
    /// .GetItemProperty�� _itemName �̿��ؼ� ItemProperty ����
    /// 2) ���� �ش� ������ ������ 0���� ũ�ٸ�
    /// 3) ���� -1 & ��ü �ı�
    /// 4) ItemAction();
    /// </summary>
    public void OnClick_ItemUse(PointerEventData data)
    {
        ItemProperty itemProperty = ItemProperty.GetItemProperty(_itemName); //�̰� �ϼ��� �ڵ������� �𸣰ھ�
        //�ƴ� ���ʿ� _itemName�� �갡 �μ��� ���� �� �ֱ�� ��? data�� �װ� �˷���?

        

        if (itemProperty.ItemNumber > 0)
        {
            itemProperty.ItemNumber--;
            Destroy(this.gameObject);
            ItemAction(); //�̰� ���ֹ����� �� �³�?
            
        }
    }

    /// <summary>
    /// 4. ItemAction:
    /// 1) switch ������ itemProperty.PropertyType �μ��� �ް� //�̰� ��� switch�� ����?
    /// 2) ItemProperty.GetItemProperty�� _itemName �̿��ؼ� ItemProperty �����ؼ�
    /// 3) Damage���, GameManager.Instance().GetCharacter("Player")�� �÷��̾� �����ؼ� ������ �߰�
    /// //�ƴ� ���ʿ� ItemList���� ������ �ִµ�?
    /// 4) Heal�̶�� �����ϰ� �����ؼ� ü�� �߰� + SceneUI�� CharacterHP() ȣ��
    /// </summary>
    public void ItemAction()
    {
        Character Player = GameManager.Instance().GetCharacter("Player");

        switch (_itemName)
        {
            case "FlameItem":
                Player._myDamage += 5;
                Debug.Log($"Your Damage Added for 5!");
                break;


            case "FireSpearItem":
                Player._myDamage += Player._myDamage/10;
                Debug.Log($"Your Damage Multiplied for 10%!");

                break;

            case "Heal":
                if (Player._myHp < Player._myHpMax-10)
                {
                    Player._myHp += 5;
                }
                UIManager.UI._sceneUI.GetComponent<SceneUI>().CharacterHp();
                Debug.Log($"Your Got Your Hp 5 back!");
                break;

            default:
                break;
        }
     }

    // 5. SetInfo: itemName�� _itemName�� �Ҵ�
    public void SetInfo(string itemName)
    {
        _itemName = itemName;
    }
}