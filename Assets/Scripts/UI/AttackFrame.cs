using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackFrame : MonoBehaviour {
    public Button[] buttonList;

    private void Start() {
        GameManager.Instance().TurnEndEvent += TurnEnd;
    }

    public void SetInteractable(bool inter) {
        foreach(Button button in buttonList) button.interactable = inter;
    }

    void TurnEnd() {
        if(GameManager.Instance().NextPlayerTurn()) StartCoroutine(EndEnum());
        else StartCoroutine(EnemyEnum());
    }

    IEnumerator EndEnum() {
        yield return new WaitForSeconds(1.3f);
        SetInteractable(true);
    }

    IEnumerator EnemyEnum() {
        yield return new WaitForSeconds(2.2f);
        GameManager.Instance().RoundNotify();
    }
}
