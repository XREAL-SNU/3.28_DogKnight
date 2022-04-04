using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    private void Start() {
        GameManager.Instance().TurnEndEvent += TurnEnd;
    }
    /// <summary>
    /// 공격 및 피격 애니메이션 실행 다 될 때까지 AttackButton 비활성화 하는 코드
    /// 이해할 필요 없음 + 건드리지 말 것
    /// </summary>
    public void Active()
    {
        //StartCoroutine(ButtonDisableCoroutine());
        GetComponent<Button>().interactable = false;
    }

    void TurnEnd() {
        if(GameManager.Instance().NextPlayerTurn()) StartCoroutine(EndEnum());
        else StartCoroutine(EnemyEnum());
    }

    IEnumerator EndEnum() {
        yield return new WaitForSeconds(1.3f);
        GetComponent<Button>().interactable = true;
    }

    IEnumerator EnemyEnum() {
        yield return new WaitForSeconds(2.2f);
        GameManager.Instance().RoundNotify();
    }
}
