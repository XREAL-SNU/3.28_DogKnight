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
        DamageItem_Flame, DamageItem_FireSpear,
        HealItem_HealStone
    }

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
        string nameItemType = _itemGroupName;
    }

    // 5. SetInfo: itemtype�� _itemGroupName�� �Ҵ�
    public void SetInfo(string itemtype)
    {
        _itemGroupName = itemtype;
    }
    
}