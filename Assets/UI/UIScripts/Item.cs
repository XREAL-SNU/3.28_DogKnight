using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;

public class Item : UIBase
{
    // 1. enum 자유롭게 구성
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

    // 2. Item Button에 OnClick_ItemUse Bind
    public override void Init()
    {
        Bind<GameObject>(typeof(Images));
        GameObject image = GetUIComponent<GameObject>((int)Images.Image);
        image.BindEvent(OnClick_ItemUse);

    }

    /// <summary>
    /// 3. OnClick_ItemUse
    /// 1) 
    /// .GetItemProperty과 _itemName 이용해서 ItemProperty 접근
    /// 2) 만약 해당 아이템 개수가 0보다 크다면
    /// 3) 개수 -1 & 객체 파괴
    /// 4) ItemAction();
    /// </summary>
    public void OnClick_ItemUse(PointerEventData data)
    {
        ItemProperty itemProperty = ItemProperty.GetItemProperty(_itemName); //이게 완성된 코드인지는 모르겠어
        //아니 애초에 _itemName을 얘가 인수로 받을 수 있기는 해? data가 그걸 알려줘?

        

        if (itemProperty.ItemNumber > 0)
        {
            itemProperty.ItemNumber--;
            Destroy(this.gameObject);
            ItemAction(); //이걸 없애버리는 게 맞냐?
            
        }
    }

    /// <summary>
    /// 4. ItemAction:
    /// 1) switch 문으로 itemProperty.PropertyType 인수로 받고 //이걸 어떻게 switch로 구현?
    /// 2) ItemProperty.GetItemProperty과 _itemName 이용해서 ItemProperty 접근해서
    /// 3) Damage라면, GameManager.Instance().GetCharacter("Player")로 플레이어 접근해서 데미지 추가
    /// //아니 애초에 ItemList에서 찢어져 있는데?
    /// 4) Heal이라면 동일하게 접근해서 체력 추가 + SceneUI의 CharacterHP() 호출
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

    // 5. SetInfo: itemName을 _itemName에 할당
    public void SetInfo(string itemName)
    {
        _itemName = itemName;
    }
}