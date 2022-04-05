using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public partial class HealthBar : MonoBehaviour//Data
{
    [SerializeField] private Slider healthBar;
}
public partial class HealthBar : MonoBehaviour//Main
{
    private void Allocate()
    {

    }
    public void Initialize()
    {
        Allocate();

    }

}
public partial class HealthBar : MonoBehaviour//Prop
{
    public void ObjectHealthUpdate(float objHealth)
    {
        healthBar.value = objHealth;
    }
}
public partial class HeaithBar : MonoBehaviour//
{

}