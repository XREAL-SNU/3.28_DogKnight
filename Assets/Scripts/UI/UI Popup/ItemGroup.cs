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
        Text _typeText = UIUtils.FindUIChild<Text>(gameObject, "ItemTypeText", true);
        _typeText.text = _itemGroupName;
        Transform itemPanel = UIUtils.FindUIChild<Transform>(gameObject, "ItemPanel");
        foreach(ItemProperty itemProperty in ItemProperty.ItemProperties)
        {
            if(itemProperty.PropertyType == _itemGroupName)
            {
                for (int i = 0; i < itemProperty.ItemNumber; i++)
                {
                    Item item = UIManager.UI.MakeSubItem<Item>(itemPanel, itemProperty.ItemName);
                    item.SetInfo(itemProperty.ItemName);
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