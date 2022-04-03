using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;
using System;

public class ItemGroup : UIBase
{
    // 1. enum 자유롭게 구성

    private string _itemGroupName;

    private void Start()
    {
        Init();
    }
    Text itemPropertyTypeText;
    // 2. 텍스트 UI에 _itemGroupName으로 update
    // 3. 현재 할당된 타입에 해당하는 아이템들을(Item) subtiem으로 생성할 것
    // 4. 생성할 때, Item의 SetInfo에 _itemName 할당해서 정보 넘겨줄 것
    public override void Init()
    {
        itemPropertyTypeText = UI_Utils.FindUIChild<Text>(gameObject, "ItemTypeText", true);
        itemPropertyTypeText.text = _itemGroupName;

        RectTransform itemPanel = UI_Utils.FindUIChild<RectTransform>(this.gameObject, "ItemPanel", true);
        foreach (var item in ItemProperty.ItemProperties)
        {
            if(item.PropertyType == _itemGroupName) //Damage == Damage or Heal == Heal
            {
                for (int i = 0; i < item.ItemNumber; i++)
                {
                    Item subItem = UIManager.UI.MakeSubItem<Item>(itemPanel.transform, item.ItemName); //Resource파일에 있는 스킬이름을 가져와서 서브아이템으로 만들어...
                    //위치는 위에서 find한 곳의 transform 위치에
                    subItem.SetInfo(item.ItemName);
                }
            }

           
        }
    }

    // 5. SetInfo: itemtype을 _itemGroupName에 할당
    public void SetInfo(string itemtype)
    {
        _itemGroupName = itemtype;
    }
}