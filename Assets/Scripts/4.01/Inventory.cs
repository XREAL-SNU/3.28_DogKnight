using UnityEngine;
using UnityEngine.EventSystems;
using XReal.XTown.UI;

public class Inventory : UIPopup
{
    //enum자유롭게 구성
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

    //  ItemList의 ItemPropertyType 참고해서 각자의 방식으로 ItemGroup subitem 만들어 볼 것
    //  생성할 때, ItemGroup의 SetInfo에 ItemPropertyType 할당해서 정보 넘겨줄 것
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GetObject((int)GameObjects.CloseButton).BindEvent(OnClick_Close);
        GameObject contentPanel = GetUIComponent<GameObject>((int)GameObjects.ContentPanel);
        //RectTransform contentPanel = UI_Utils.FindUIChild<RectTransform>(this.gameObject, "ContentPanel", true);

        //이렇게하는게 아닌거 같은데..?ㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠ
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

