using UnityEngine;
using UnityEngine.EventSystems;
using XReal.XTown.UI;

public class Inventory : UIPopup
{
    enum GameObjects
    {
        CloseButton,
        ContentPanel
    }

    private void Start()
    {
        Init();
    }

    public int ItemNum = 30;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GetObject((int)GameObjects.CloseButton).BindEvent(OnClick_Close);
        GameObject contentPanel = GetUIComponent<GameObject>((int)GameObjects.ContentPanel);


        for (int i = 0; i < ItemNum; i++)
        {
            string name = "Item" + i;
            GameObject item = UIManager.UI.MakeSubItem<Item>(contentPanel.transform).gameObject;
            Item itemscript = item.GetOrAddComponent<Item>();
            itemscript.SetInfo(name, i * 10);
        }
    }

    public void OnClick_Close(PointerEventData data)
    {
        ClosePopup();
    }
}

//public class Inventory : UIPopup
//{
//    // 1. enum 자유롭게 구성

//    private void Start()
//    {
//        Init();
//    }

//    // 2. Popup UI 닫는 버튼에 OnClick_Close 바인드
//    // 3. ItemList의 ItemPropertyType 참고해서 각자의 방식으로 ItemGroup subitem 만들어 볼 것
//    // 4. 생성할 때, ItemGroup의 SetInfo에 ItemPropertyType 할당해서 정보 넘겨줄 것
//    public override void Init()
//    {
//        base.Init();
//    }

//    // 5. OnClick_Close: Popup 닫기
//    public void OnClick_Close(PointerEventData data)
//    {

//    }
//}