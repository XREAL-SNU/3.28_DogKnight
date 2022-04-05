using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public partial class UseItem : MonoBehaviour//Data
{
    [SerializeField] private UnityEvent callUseItem;
}

public partial class UseItem : MonoBehaviour//Main
{
    private void Allocate()
    {

    }
    public void Initialize()
    {
        Allocate();

    }
}

public partial class UseItem : MonoBehaviour//Prop
{
    public void CallUseItem()
    {
        callUseItem.Invoke();
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
