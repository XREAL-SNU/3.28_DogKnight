using UnityEngine;
using UnityEngine.EventSystems;
using XReal.XTown.UI;

public class Inventory : UIPopup
{
    //enum�����Ӱ� ����
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

    //  ItemList�� ItemPropertyType �����ؼ� ������ ������� ItemGroup subitem ����� �� ��
    //  ������ ��, ItemGroup�� SetInfo�� ItemPropertyType �Ҵ��ؼ� ���� �Ѱ��� ��
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GetObject((int)GameObjects.CloseButton).BindEvent(OnClick_Close);
        GameObject contentPanel = GetUIComponent<GameObject>((int)GameObjects.ContentPanel);
        //RectTransform contentPanel = UI_Utils.FindUIChild<RectTransform>(this.gameObject, "ContentPanel", true);

        //�̷����ϴ°� �ƴѰ� ������..?�ФФФФФФФФФФФФФФФФ�
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

