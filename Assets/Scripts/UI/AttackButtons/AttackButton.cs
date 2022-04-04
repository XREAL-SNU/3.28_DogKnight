using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour {
    void Start() {
        GetComponent<Button>().onClick.AddListener(_OnClicked);
    }

    private void _OnClicked() {
        Clicked();
        transform.parent.GetComponent<AttackFrame>().SetInteractable(false);
    }

    public virtual void Clicked() {
        UI.SelectTarget(transform.position, GameManager.Instance().TeamMember(!GameManager.Instance().NextPlayerTurn()),
            t => GameManager.Instance().RoundNotify(c => c.Attack(t)));
    }
}
