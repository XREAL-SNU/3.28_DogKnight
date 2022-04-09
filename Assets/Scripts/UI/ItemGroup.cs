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

    // 2. �ؽ�Ʈ UI�� _itemGroupName���� update
    // 3. ���� �Ҵ�� Ÿ�Կ� �ش��ϴ� �����۵���(Item) subtiem���� ������ ��
    // 4. ������ ��, Item�� SetInfo�� _itemName �Ҵ��ؼ� ���� �Ѱ��� ��
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

    // 5. SetInfo: itemtype�� _itemGroupName�� �Ҵ�
    public void SetInfo(string itemtype)
    {
        this.ItemGroupName = itemtype;
    }
}