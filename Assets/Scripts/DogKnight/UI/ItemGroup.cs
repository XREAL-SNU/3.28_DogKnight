using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;
using System;

public class ItemGroup : UIBase
{
    // 1. enum �����Ӱ� ����
    enum Texts
    {
        ItemTypeText
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

    // 2. �ؽ�Ʈ UI�� _itemGroupName���� update
    // 3. ���� �Ҵ�� Ÿ�Կ� �ش��ϴ� �����۵���(Item) subtiem���� ������ ��
    // 4. ������ ��, Item�� SetInfo�� _itemName �Ҵ��ؼ� ���� �Ѱ��� ��
    public override void Init()
    {
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        GetText((int)Texts.ItemTypeText).text = _itemGroupName;
        GameObject itemPanel = GetUIComponent<GameObject>((int)GameObjects.ItemPanel);

        foreach (ItemProperty itemproperty in ItemProperty.ItemProperties)
        {
            if(itemproperty.ItemName == _itemGroupName)
            {
                for(int i=0;i<itemproperty.ItemNumber;i++)
                {
                    Item item = UIManager.UI.MakeSubItem<Item>(itemPanel.transform, _itemGroupName);
                    item.SetInfo(_itemGroupName);
                }
            }
        }
    }

    // 5. SetInfo: itemtype�� _itemGroupName�� �Ҵ�
    public void SetInfo(string itemtype)
    {
        _itemGroupName = itemtype;
    }
}