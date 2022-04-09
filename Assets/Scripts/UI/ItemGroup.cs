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
    enum GameObjects
    {
        ItemPanel,
        BackgroundImage
    }
    enum Texts
    {
        ItemTypeText
    }

    private string ItemGroupName;
    private int Number = 0;



    private void Start()
    {
        Init();
    }

    // 2. 텍스트 UI에 _itemGroupName으로 update
    // 3. 현재 할당된 타입에 해당하는 아이템들을(Item) subtiem으로 생성할 것
    // 4. 생성할 때, Item의 SetInfo에 _itemName 할당해서 정보 넘겨줄 것
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Bind<Text>(typeof(Texts));

        GetText((int)Texts.ItemTypeText).text = ItemGroupName;
        GameObject itemText = GetObject((int)Texts.ItemTypeText);

        foreach (ItemProperty itemproperty in ItemProperty.ItemProperties)
        {
            if (itemproperty.PropertyType == Enum.Parse(typeof(ItemPropertyType), ItemGroupName).ToString())
            {
                for (int i = 0; i < itemproperty.ItemNumber; i++)
                {
                    GameObject item = UIManager.UI.MakeSubItem<Item>(itemText.transform, itemproperty.ItemName).gameObject;
                    Item itemscript = item.GetOrAddComponent<Item>();
                    itemscript.SetInfo(itemproperty);
                }
            }
        }
    }

    // 5. SetInfo: itemtype을 _itemGroupName에 할당
    public void SetInfo(string itemtype)
    {
        this.ItemGroupName = itemtype;
    }
}