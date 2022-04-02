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
    enum GameObjects{
        Blocker,
        Background,
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

        GameObject CloseButton = GetUIComponent<GameObject>((int)GameObjects.CloseButton);
        CloseButton.BindEvent(OnClick_Close);
       
        //������ Ÿ�Ժ� itemGroup itemcontentPanel�ؿ� ����
        //1) itemŸ�Ժ� itemGroup instentiate
        //2) item�� item Group�� �ֱ�
        GameObject contentPanel = UIUtils.FindUIChild(gameObject, "ContentPanel", true);
        foreach (string typeName in Enum.GetNames(typeof(ItemPropertyType))){
            ItemGroup _itemGroup = UIManager.UI.MakeSubItem<ItemGroup>(contentPanel.transform, "ItemGroup");
            _itemGroup.SetInfo(typeName);
        }

        
    }

    // 5. OnClick_Close: Popup �ݱ�
    public void OnClick_Close(PointerEventData data)
    {
        UIManager.UI.ClosePopupUI();
    }
}