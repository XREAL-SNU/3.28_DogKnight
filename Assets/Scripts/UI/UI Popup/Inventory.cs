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
    enum GameObjects{
        Blocker,
        Background,
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

        GameObject CloseButton = GetUIComponent<GameObject>((int)GameObjects.CloseButton);
        CloseButton.BindEvent(OnClick_Close);
       
        //아이템 타입별 itemGroup itemcontentPanel밑에 생성
        //1) item타입별 itemGroup instentiate
        //2) item들 item Group에 넣기
        GameObject contentPanel = UIUtils.FindUIChild(gameObject, "ContentPanel", true);
        foreach (string typeName in Enum.GetNames(typeof(ItemPropertyType))){
            ItemGroup _itemGroup = UIManager.UI.MakeSubItem<ItemGroup>(contentPanel.transform, "ItemGroup");
            _itemGroup.SetInfo(typeName);
        }

        
    }

    // 5. OnClick_Close: Popup 닫기
    public void OnClick_Close(PointerEventData data)
    {
        UIManager.UI.ClosePopupUI();
    }
}