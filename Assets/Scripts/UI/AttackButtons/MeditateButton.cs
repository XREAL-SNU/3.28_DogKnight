using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeditateButton : AttackButton {
    public override void Clicked() {
        GameManager.Instance().RoundNotify(c => {
            if (c is Player p) p.Meditate();
            else c.EndAttack();
        });
    }
}
