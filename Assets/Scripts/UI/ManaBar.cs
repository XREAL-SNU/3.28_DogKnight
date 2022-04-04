using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManaBar : MonoBehaviour {
    private const float MPBAR_WIDTH = 583f;

    public RectTransform mpBar, mpBarSub;
    public TextMeshProUGUI mpText;

    private float deltaMp = 0f;

    void Update() {
        if (Mathf.Abs(deltaMp - Player.mp) < 0.01f) deltaMp = Player.mp;
        else deltaMp = Mathf.Lerp(deltaMp, Player.mp, (Player.mp >= deltaMp ? 0.08f * 2f : 0.08f) * 60f * Time.deltaTime);
        if (deltaMp <= Player.mp) {
            //healing
            setMPSub(deltaMp);
            setMP(deltaMp);
        }
        else {
            //taken damage
            setMPSub(deltaMp);
            setMP(Player.mp);
        }

        mpText.text = Mathf.CeilToInt(Mathf.Clamp(Player.mp, 0, Player.maxMp)) + "";
    }

    private void setMP(float mp) {
        mpBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, MPBAR_WIDTH * Mathf.Clamp01(mp / Player.maxMp));
    }

    private void setMPSub(float mp) {
        mpBarSub.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, MPBAR_WIDTH * Mathf.Clamp01(mp / Player.maxMp));
    }
}
