using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using XReal.XTown.UI;

public class TestScript : UIScene
{
    enum GameObjects
    {
        Button
    }
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();//∞Ì¡§
        Bind<GameObject>(typeof(GameObjects));

        GameObject button = GetUIComponent<GameObject>((int)GameObjects.Button);
        button.BindEvent(OnClick_Inventory);
        button.BindEvent(OnButtonEnter, UIEvents.UIEvent.Enter);
    }

    public void OnClick_Inventory(PointerEventData data)
    {
        Debug.Log("Click InventoryButton");
    }
    public void OnButtonEnter(PointerEventData data)
    {
        Debug.Log(data.pointerEnter.name + " Enter!");
    }
}
