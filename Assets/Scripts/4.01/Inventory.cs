using UnityEngine;
using UnityEngine.EventSystems;
using XReal.XTown.UI;

public class Inventory : UIPopup
{
    //enum切政罫惟 姥失
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

    //  ItemList税 ItemPropertyType 凧壱背辞 唖切税 号縦生稽 ItemGroup subitem 幻級嬢 瑳 依
    //  持失拝 凶, ItemGroup税 SetInfo拭 ItemPropertyType 拝雁背辞 舛左 角移匝 依
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GetObject((int)GameObjects.CloseButton).BindEvent(OnClick_Close);
        GameObject contentPanel = GetUIComponent<GameObject>((int)GameObjects.ContentPanel);
        //RectTransform contentPanel = UI_Utils.FindUIChild<RectTransform>(this.gameObject, "ContentPanel", true);

        //戚係惟馬澗惟 焼観暗 旭精汽..?ばばばばばばばばばばばばばばばばば
        GameObject attackItemGroup = UIManager.UI.MakeSubItem<ItemGroup>(contentPanel.transform).gameObject;
        ItemGroup attackGruopScript = attackItemGroup.GetOrAddComponent<ItemGroup>();
        attackGruopScript.SetInfo(ItemPropertyType.Damage.ToString());

        GameObject healItemGroup = UIManager.UI.MakeSubItem<ItemGroup>(contentPanel.transform).gameObject;
        ItemGroup healGruopScript = healItemGroup.GetOrAddComponent<ItemGroup>();
        healGruopScript.SetInfo(ItemPropertyType.Heal.ToString());
    }

    public void OnClick_Close(PointerEventData data)
    {
        ClosePopup();
    }
}

