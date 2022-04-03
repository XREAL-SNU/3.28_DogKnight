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