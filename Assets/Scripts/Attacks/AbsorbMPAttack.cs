using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttack", menuName = "Attacks/MPAttack", order = 3)]
public class AbsorbMPAttack : Attack {
    public float mpBase = -2f;
    public float mpExtra = 5f;

    public GameObject mpEffect;

    public override void OnHit(Character c, Character target, float damage) {
        base.OnHit(c, target, damage);

        if (c is Player) {
            float amount = mpBase + Random.Range(0, mpExtra);
            if (amount > 0f) {
                Player.mp = Mathf.Min(Player.maxMp, Player.mp + amount);
                if (mpEffect != null) Instantiate(mpEffect, c.transform.position + Vector3.up * 0.5f, Quaternion.identity);
            }
        }
    }
}
