using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttack", menuName = "Attacks/SplashAttack", order = 3)]
public class SplashAttack : Attack {

    public override void At(Character c, Character target) {
        if (target == null) {
            GameManager.Instance().TeamMember(!GameManager.Instance().TeamMember(true).Contains(c)).ForEach(m => {
                c.StartCoroutine(AttackEnum(c, m));
            });
        }
        else {
            c.StartCoroutine(AttackEnum(c, target));
        }
        c.StartCoroutine(EndEnum(c));
    }

    IEnumerator AttackEnum(Character c, Character target) {
        yield return new WaitForSeconds(duration);
        OnHit(c, target, damage);

        for (int i = 0; i < shots - 1; i++) {
            if (!animSpacingTrigger.Equals("")) {
                c.Animate(animSpacingTrigger);
            }
            yield return new WaitForSeconds(spacing);
            OnHit(c, target, damage);
        }
    }

    IEnumerator EndEnum(Character c) {
        yield return new WaitForSeconds(duration + (shots - 1) * spacing);
        c.EndAttack();
    }
}
