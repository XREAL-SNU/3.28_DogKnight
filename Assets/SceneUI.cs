using UnityEngine;
using UnityEngine.EventSystems;
using XReal.XTown.UI;

public class SceneUI : UIScene
{
    enum GameObjects
    {
        InventoryButton,
        MinimapButton
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject inventoryButton = GetUIComponent<GameObject>((int)GameObjects.InventoryButton);
        inventoryButton.BindEvent(OnClick_Inventory);
        inventoryButton.BindEvent(OnButtonEnter, UIEvents.UIEvent.Enter);

        GetObject((int)GameObjects.MinimapButton).BindEvent(OnClick_Minimap);
        GetObject((int)GameObjects.MinimapButton).BindEvent(OnButtonEnter, UIEvents.UIEvent.Enter);
    
    
    
    
    }

    public void OnClick_Inventory(PointerEventData data)
    {
        Debug.Log("Click InventoryButton");
    }

    public void OnClick_Minimap(PointerEventData data)
    {
        Debug.Log("Click MinimapButton");
        UIManager.UI.ShowPopupUI<UIPopup>("Minimap");
    }

    public void OnButtonEnter(PointerEventData data)
    {
        Debug.Log(data.pointerEnter.name + " Enter!");
    }
}