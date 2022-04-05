using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;

public class Inventory : UIPopup
{
    // 1. enum 자유롭게 구성
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
    
    // 2. Popup UI 닫는 버튼에 OnClick_Close 바인드
    // 3. ItemList의 ItemPropertyType 참고해서 각자의 방식으로 ItemGroup subitem 만들어 볼 것
    // 4. 생성할 때, ItemGroup의 SetInfo에 ItemPropertyType 할당해서 정보 넘겨줄 것
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject inventoryContent = GetUIComponent<GameObject>((int)GameObjects.InventoryContent);

        GameObject closeButton = inventoryContent.transform.GetChild(2).gameObject;
        Debug.Log(closeButton);
        closeButton.BindEvent(OnClick_Close);

        Transform content = inventoryContent.transform.GetChild(0).GetChild(0);

        // flame item 추가
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

        // heal item 추가
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

    // 5. OnClick_Close: Popup 닫기
    public void OnClick_Close(PointerEventData data)
    {
        ClosePopup();
    }
}