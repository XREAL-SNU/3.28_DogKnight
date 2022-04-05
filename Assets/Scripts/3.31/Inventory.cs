using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;

public class Inventory : UIPopup
{
    // 1. enum �����Ӱ� ����
    enum GameObjects
    {
        InventoryContent
    }

    private void Start()
    {
        Init();
    }

    private int flameItemNum = 10;
    private int healItemNum = 8;
    
    // 2. Popup UI �ݴ� ��ư�� OnClick_Close ���ε�
    // 3. ItemList�� ItemPropertyType �����ؼ� ������ ������� ItemGroup subitem ����� �� ��
    // 4. ������ ��, ItemGroup�� SetInfo�� ItemPropertyType �Ҵ��ؼ� ���� �Ѱ��� ��
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject inventoryContent = GetUIComponent<GameObject>((int)GameObjects.InventoryContent);

        GameObject closeButton = inventoryContent.transform.GetChild(2).gameObject;
        Debug.Log(closeButton);
        closeButton.BindEvent(OnClick_Close);

        Transform content = inventoryContent.transform.GetChild(0).GetChild(0);

        // flame item �߰�
        ItemGroup flameItemGroup =  UIManager.UI.MakeSubItem<ItemGroup>(content, "ItemGroup");
        flameItemGroup.gameObject.AddComponent<LayoutElement>();
        flameItemGroup.GetComponent<LayoutElement>().preferredHeight = 500;
        flameItemGroup.GetComponent<LayoutElement>().preferredWidth = 1300;


        GameObject flameItemGroupObject = flameItemGroup.gameObject;
        flameItemGroup.SetInfo("Damage");

        for (int i = 0; i < flameItemNum; i++)
        {
            string name = "FlameItem";
            GameObject item = UIManager.UI.MakeSubItem<Item>(flameItemGroupObject.transform.GetChild(1).transform, name).gameObject;

            Item itemscript = item.GetOrAddComponent<Item>();
            itemscript.SetInfo(name);
        }

        // heal item �߰�
        ItemGroup healItemGroup = UIManager.UI.MakeSubItem<ItemGroup>(content, "ItemGroup1");
        healItemGroup.gameObject.AddComponent<LayoutElement>();
        healItemGroup.GetComponent<LayoutElement>().preferredHeight = 900;
        healItemGroup.GetComponent<LayoutElement>().preferredWidth = 1300;

        GameObject healItemGroupObject = healItemGroup.gameObject;
        healItemGroup.SetInfo("Heal");
 
        for (int i = 0; i < healItemNum; i++)
        {
            string name = "HealItem";
            GameObject item = UIManager.UI.MakeSubItem<Item>(healItemGroupObject.transform.GetChild(1).transform, name).gameObject;
            Item itemscript = item.GetOrAddComponent<Item>();
            itemscript.SetInfo(name);
        }
    }

    // 5. OnClick_Close: Popup �ݱ�
    public void OnClick_Close(PointerEventData data)
    {
        ClosePopup();
    }
}