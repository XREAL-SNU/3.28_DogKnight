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
        CloseButton, ContentPanel, Blocker
    }

    private void Start()
    {
        Init();
    }

    // 2. Popup UI �ݴ� ��ư�� OnClick_Close ���ε�
    // 3. ItemList�� ItemPropertyType �����ؼ� ������ ������� ItemGroup subitem ����� �� ��
    // 4. ������ ��, ItemGroup�� SetInfo�� ItemPropertyType �Ҵ��ؼ� ���� �Ѱ��� ��
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject closeButton = GetUIComponent<GameObject>((int)GameObjects.CloseButton);
        closeButton.BindEvent(OnClick_Close);

        //GameObject contentPanel = GetUIComponent<GameObject>((int)GameObjects.ContentPanel);

        /*foreach (string typeName in Enum.GetNames(typeof(ItemPropertyType)))
        {
            ItemGroup _itemGroup = UIManager.UI.MakeSubItem<ItemGroup>(contentPanel.transform, "ItemGroup");
            _itemGroup.SetInfo(typeName);
        }*/
    }

    // 5. OnClick_Close: Popup �ݱ�
    public void OnClick_Close(PointerEventData data)
    {
        UIManager.UI.ClosePopupUI();
    }
}