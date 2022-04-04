using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttack", menuName = "Attacks/Attack", order = 2)]
public class Attack : ScriptableObject {
    public float damage = 20f;
    public float extraDamage = 0f;
    public float strengthMultiplier = 1f;

    public float duration = 1f; //duration until first hit
    public string animTrigger = "";

    public int shots = 1;
    public float spacing = 0.2f; //duration between subsequent shots
    public string animSpacingTrigger = "";

    public GameObject hitFx = null;
    public float fxOffset = 0.2f;

    public virtual void At(Character c, Character target) {
        if (!animTrigger.Equals("")) {
            c.Animate(animTrigger);
        }

        if (Valid(c, target)) {
            c.StartCoroutine(Hitter(c, target, GetDamage(c), 1f));
        }
    }

    public virtual bool Valid(Character c, Character target) {
        return true;
    }

    public virtual float GetDamage(Character c) {
        return (damage + Random.Range(0, extraDamage)) * Mathf.Lerp(1f, c.strength, strengthMultiplier);
    }

    public virtual void OnHit(Character c, Character target, float damage) {
        if (hitFx != null) {
            Instantiate(hitFx, target.transform.position + new Vector3(Random.Range(-fxOffset, fxOffset), 0.5f + Random.Range(-fxOffset, fxOffset), Random.Range(-fxOffset, fxOffset)), Quaternion.identity);
        }
        target.GetHit(GetDamage(c));
    }

    IEnumerator Hitter(Character c, Character target, float damage, float speed) {
        yield return new WaitForSeconds(duration * speed);
        OnHit(c, target, damage);

        for(int i = 0; i < shots - 1; i++) {
            if (!animSpacingTrigger.Equals("")) {
                c.Animate(animSpacingTrigger);
            }
            yield return new WaitForSeconds(spacing * speed);
            Debug.Log("Hit!");
            OnHit(c, target, damage);
        }

        c.EndAttack();
    }
}
