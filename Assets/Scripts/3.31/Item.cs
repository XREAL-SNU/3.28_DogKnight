using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;

public class Item : UIBase
{
    // 1. enum 자유롭게 구성

    private string _itemName;

    private void Start()
    {
        Init();
    }

    // 2. Item Button에 OnClick_ItemUse Bind
    public override void Init()
    {
        
    }

    /// <summary>
    /// 3. OnClick_ItemUse
    /// 1) ItemProperty.GetItemProperty과 _itemName 이용해서 ItemProperty 접근
    /// 2) 만약 해당 아이템 개수가 0보다 크다면
    /// 3) 개수 -1 & 객체 파괴
    /// 4) ItemAction();
    /// </summary>
    public void OnClick_ItemUse(PointerEventData data)
    {
        
    }

    /// <summary>
    /// 4. ItemAction:
    /// 1) switch 문으로 itemProperty.PropertyType 인수로 받고
    /// 2) ItemProperty.GetItemProperty과 _itemName 이용해서 ItemProperty 접근해서
    /// 3) Damage라면, GameManager.Instance().GetCharacter("Player")로 플레이어 접근해서 데미지 추가
    /// 4) Heal이라면 동일하게 접근해서 체력 추가 + SceneUI의 CharacterHP() 호출
    /// </summary>
    public void ItemAction()
    {
        
    }

    // 5. SetInfo: itemName을 _itemName에 할당
    public void SetInfo(string itemName)
    {
        _itemName = itemName;
    }
}
