using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;

public class Item : UIBase
{
    enum Images
    {
        Image
    }

    enum Texts
    {
        Text
    }

    private string _name;
    private int _color;

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));

        GetText((int)Texts.Text).text = _name;
        GetImage((int)Images.Image).color = new Color(0, _color / 255f, 0);
        GetImage((int)Images.Image).gameObject.BindEvent(OnClick_Item);
    }

    public void SetInfo(string name, int color)
    {
        this.name = name;
        _name = name;
        _color = color;
    }

    public void OnClick_Item(PointerEventData data)
    {
        Debug.Log(this.name + " is Click!");
    }
}