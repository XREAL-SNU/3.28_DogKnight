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
    /// ���� �� �ǰ� �ִϸ��̼� ���� �� �� ������ AttackButton ��Ȱ��ȭ �ϴ� �ڵ�
    /// ������ �ʿ� ���� + �ǵ帮�� �� ��
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
