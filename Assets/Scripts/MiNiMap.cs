using UnityEngine;
using UnityEngine.EventSystems;
using XReal.XTown.UI;

public class MiNiMap : UIPopup
{
    enum GameObjects
    {
        CloseButton,
        Panel,
        Mouse
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GetObject((int)GameObjects.CloseButton).BindEvent(OnClick_Close);
        GetObject((int)GameObjects.Panel).BindEvent(OnEnter_Panel, UIEvents.UIEvent.Drag);
    }

    public void OnEnter_Panel(PointerEventData data)
    {
        GetObject((int)GameObjects.Mouse).transform.position = data.position;
    }

    public void OnClick_Close(PointerEventData data)
    {
        ClosePopup();
    }
}