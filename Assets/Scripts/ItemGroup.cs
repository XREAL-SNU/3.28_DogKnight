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
    enum GameObjects
    {

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
        Text itemText = UIUtils.FindUIChild<Text>(gameObject, "ItemTypeText", true);
        itemText.text = _itemGroupName;
        Transform itemPanel = UIUtils.FindUIChild<Transform>(gameObject, "ItemPanel", true);

        foreach (ItemProperty i in ItemProperty.ItemProperties)
        {
            if (i.PropertyType == _itemGroupName)
            {
                for (int j = 0; j < i.ItemNumber; j++)
                {
                    Item item = UIManager.UI.MakeSubItem<Item>(itemPanel, i.ItemName);
                    item.SetInfo(i.ItemName);
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
