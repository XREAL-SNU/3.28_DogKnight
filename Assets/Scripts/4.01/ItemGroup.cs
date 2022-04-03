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
    Text itemPropertyTypeText;
    // 2. �ؽ�Ʈ UI�� _itemGroupName���� update
    // 3. ���� �Ҵ�� Ÿ�Կ� �ش��ϴ� �����۵���(Item) subtiem���� ������ ��
    // 4. ������ ��, Item�� SetInfo�� _itemName �Ҵ��ؼ� ���� �Ѱ��� ��
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
                    Item subItem = UIManager.UI.MakeSubItem<Item>(itemPanel.transform, item.ItemName); //Resource���Ͽ� �ִ� ��ų�̸��� �����ͼ� ������������� �����...
                    //��ġ�� ������ find�� ���� transform ��ġ��
                    subItem.SetInfo(item.ItemName);
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