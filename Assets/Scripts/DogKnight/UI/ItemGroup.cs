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
    enum Texts
    {
        Text
    }

    enum GameObjects
    {
        ItemPanel
    }

    private string _itemGroupName;

    private void Start()
    {
        Init();
    }

    // 2. 텍스트 UI에 _itemGroupName으로 update
    // 3. 현재 할당된 타입에 해당하는 아이템들을(Item) subtiem으로 생성할 것
    // 4. 생성할 때, Item의 SetInfo에 _itemName 할당해서 정보 넘겨줄 것
    public override void Init()
    {
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        GetText((int)Texts.Text).text = _itemGroupName;

        for (int i = 0; i < ItemNum; i++)
        {
            string name = "Item" + i;
            GameObject item = UIManager.UI.MakeSubItem<Item>(ItemPanel.transform).gameObject;
            Item itemscript = item.GetOrAddComponent<Item>();
            itemscript.SetInfo(_itemGroupName);
        }
    }

    // 5. SetInfo: itemtype을 _itemGroupName에 할당
    public void SetInfo(string itemtype)
    {
        _itemGroupName = itemtype;
    }
}