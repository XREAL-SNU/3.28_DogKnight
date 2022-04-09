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

    // 2. Popup UI �ݴ� ��ư�� OnClick_Close ���ε�
    // 3. ItemList�� ItemPropertyType �����ؼ� ������ ������� ItemGroup subitem ����� �� ��
    // 4. ������ ��, ItemGroup�� SetInfo�� ItemPropertyType �Ҵ��ؼ� ���� �Ѱ��� ��
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

    // 5. OnClick_Close: Popup �ݱ�
    public void OnClick_Close(PointerEventData data)
    {
        ClosePopup();
    }
}