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
        ContentPanel
    }
    enum Buttons
    {
        CloseButton
    }




    private void Start()
    {
        Init();
    }

    // 2. Popup UI 닫는 버튼에 OnClick_Close 바인드
    // 3. ItemList의 ItemPropertyType 참고해서 각자의 방식으로 ItemGroup subitem 만들어 볼 것
    // 4. 생성할 때, ItemGroup의 SetInfo에 ItemPropertyType 할당해서 정보 넘겨줄 것
    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));
        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnClick_Close);

        GameObject panel = GetObject((int)GameObjects.ContentPanel);

        foreach (ItemPropertyType type in Enum.GetValues(typeof(ItemPropertyType)))
        {
            GameObject itemgroup = UIManager.UI.MakeSubItem<ItemGroup>(panel.transform).gameObject;
            ItemGroup itemgroupscript = itemgroup.GetOrAddComponent<ItemGroup>();
            itemgroupscript.SetInfo(type.ToString());
        };


    }

    // 5. OnClick_Close: Popup 닫기
    public void OnClick_Close(PointerEventData data)
    {
        ClosePopup();
    }
}