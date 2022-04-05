using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;
using System;

public class ItemGroup1 : UIBase
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
        transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Text>().text = _itemGroupName;
    }

    // 5. SetInfo: itemtype�� _itemGroupName�� �Ҵ�
    public void SetInfo(string itemtype)
    {
        _itemGroupName = itemtype;
    }
}
