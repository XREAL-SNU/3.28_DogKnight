using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public partial class NameTag : MonoBehaviour//Data
{
    private string objectName;
    [SerializeField]private UnityEvent getObjectName;
}
public partial class NameTag : MonoBehaviour//Main
{
    private void Start()
    {
        getObjectName.Invoke();
    }
}
public partial class NameTag : MonoBehaviour//Prop
{
    public void ReceiveObjectName(int mynum)
    {
        objectName = "" + mynum;
    }
}
public partial class NameTag : MonoBehaviour//
{

}