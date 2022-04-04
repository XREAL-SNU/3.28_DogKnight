using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal", menuName = "Attacks/Heal", order = 3)]
public class Heal : Attack {
    public float strengthBuff = 0f;

    public override void At(Character c, Character target) {
        if (target == null) {
            GameManager.Instance().TeamMember(GameManager.Instance().TeamMember(true).Contains(c)).ForEach(m => {
                c.StartCoroutine(HealEnum(c, m));
            });
        }
        else {
            c.StartCoroutine(HealEnum(c, target));
        }
        c.StartCoroutine(EndEnum(c));
    }

    IEnumerator HealEnum(Character c, Character target) {
        if (hitFx != null) {
            Instantiate(hitFx, target.transform.position + Vector3.up * 0.5f, Quaternion.identity);
        }
        yield return new WaitForSeconds(duration);

        target.Heal(GetDamage(c));
        target.strength += strengthBuff;
    }

    IEnumerator EndEnum(Character c) {
        yield return new WaitForSeconds(duration);
        c.EndAttack();
    }
}
