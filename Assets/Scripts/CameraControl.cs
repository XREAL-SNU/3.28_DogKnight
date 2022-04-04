using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CameraControl : MonoBehaviour {
    public LookAtConstraint look;
    public Transform center;

    void Start() {
        look = GetComponent<LookAtConstraint>();
        GameManager.Instance().TurnEvent += Turn;

        ConstraintSource cs = new ConstraintSource();
        cs.sourceTransform = GameManager.Instance().players[0].transform;
        cs.weight = 0;
        look.AddSource(cs);
    }

    void Update() {
        if(look.sourceCount > 1) {
            float f = look.GetSource(0).weight;
            f = Mathf.Lerp(f, 0f, 60f * 0.04f * Time.deltaTime * look.sourceCount);
            if (f < 0.001f) f = 0f;

            ConstraintSource c0 = look.GetSource(0); c0.weight = f;
            ConstraintSource c1 = look.GetSource(1); c1.weight = 1 - f;
            look.SetSource(0, c0);
            look.SetSource(1, c1);

            if (f == 0f) {
                look.RemoveSource(0);
            }
        }
    }

    public void Turn(int round, string turn, Character character) {
        if(GameManager.Instance().NextPlayerTurn()) StartCoroutine(_LookAt(GameManager.Instance().NextCharacter().transform));
        else {
            ConstraintSource c = new ConstraintSource();
            c.sourceTransform = center;
            c.weight = 0;
            look.AddSource(c);
        }
    }

    IEnumerator _LookAt(Transform t) {
        ConstraintSource c = new ConstraintSource();
        c.sourceTransform = center;
        c.weight = 0;
        look.AddSource(c);
        yield return new WaitForSeconds(2.5f);
        ConstraintSource cs = new ConstraintSource();
        cs.sourceTransform = t;
        cs.weight = 0;
        look.AddSource(cs);
    }
}
