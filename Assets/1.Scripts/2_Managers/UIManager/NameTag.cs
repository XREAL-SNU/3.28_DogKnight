using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public partial class NameTag : MonoBehaviour//Data
{
    public Text objectName;
    [SerializeField]private UnityEvent getObjectName;
}
public partial class NameTag : MonoBehaviour//Main
{
    private void Start()
    {
        getObjectName.Invoke();
    }
    private void Allocate()
    {

    }
    public void Initialize()
    {
        Allocate();

    }
}
public partial class NameTag : MonoBehaviour//Prop
{
    public void ReceiveObjectName(int mynum)
    {
        objectName.text = ""+mynum;
    }
}
public partial class NameTag : MonoBehaviour//
{

}