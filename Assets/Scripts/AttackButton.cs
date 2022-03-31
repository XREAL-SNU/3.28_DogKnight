using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    /// <summary>
    /// ?? ? ?? ????? ?? ? ? ??? AttackButton ???? ?? ??
    /// ??? ?? ?? + ???? ? ?
    /// </summary>
    public void Active()
    {
        StartCoroutine(ButtonDisableCoroutine());
    }

    IEnumerator ButtonDisableCoroutine()
    {
        Button attackButton = this.GetComponent<Button>();
        attackButton.interactable = false;
        yield return new WaitForSeconds(2.5f);
        attackButton.interactable = true;
    }
}
