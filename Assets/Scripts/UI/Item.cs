using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;

public class Item : UIBase
{
    enum GameObjects
    {
        BackgroundImage,
        ItemImage
    }

    private ItemProperty itemProperty;
    private SceneUI sceneUI;

    private void Start()
    {
        Init();
        sceneUI = GameObject.Find("SceneUI").GetComponent<SceneUI>();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

        GetObject((int)GameObjects.ItemImage).BindEvent(OnClick_ItemUse);

    }

    public void OnClick_ItemUse(PointerEventData data)
    {
        if (ItemProperty.GetItemProperty(this.itemProperty.ItemName).ItemNumber >= 1)
        {
            ItemProperty.GetItemProperty(this.itemProperty.ItemName).ItemNumber -= 1;
            ItemAction();
            Destroy(gameObject);
        }
    }

    public void ItemAction()
    {
        switch (itemProperty.PropertyType.ToString())
        {
            case "Damage":
                GameManager.Instance().GetCharacter("Player")._myDamage += itemProperty.ItemAction;
                Debug.Log($"Player Damge Up : {itemProperty.ItemAction}");
                break;
            case "Heal":
                GameManager.Instance().GetCharacter("Player")._myHp += itemProperty.ItemAction;
                if (GameManager.Instance().GetCharacter("Player")._myHp > GameManager.Instance().GetCharacter("Player")._myHpMax)
                {
                    GameManager.Instance().GetCharacter("Player")._myHp = GameManager.Instance().GetCharacter("Player")._myHpMax;
                }
                sceneUI.CharacterHp();
                Debug.Log($"Player Hp UP : {itemProperty.ItemAction}");
                break;
        }
    }

    public void SetInfo(ItemProperty itemProperty)
    {
        this.itemProperty = itemProperty;
    }
}
