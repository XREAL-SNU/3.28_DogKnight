using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    /// <summary>
    /// 공격 및 피격 애니메이션 실행 다 될 때까지 AttackButton 비활성화 하는 코드
    /// 이해할 필요 없음 + 건드리지 말 것
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
