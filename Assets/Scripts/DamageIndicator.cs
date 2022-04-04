using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour {
    RectTransform rect, canvas;
    Camera cam;
    TextMeshProUGUI text;

    Transform parent;
    Vector2 initPos, offset;
    public float time;
    public Func<float, float> interp;

    public static Func<float, float> bounce = (t) => t > 0.5f ? 0f : 2 * t * (1 - 2 * t) * 0.4f,
        fin = (t) => t * 0.2f;

    void Start() {
        rect = GetComponent<RectTransform>();
        cam = Camera.main;
        canvas = transform.parent.GetComponent<RectTransform>();
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        text.enabled = true;
        time += Time.deltaTime;

        rect.anchoredPosition = Pos() + new Vector2(0f, interp(time / 2f) * canvas.sizeDelta.y);

        if(time > 1f) {
            Color c = text.color;
            c.a = 2f - time;
            text.color = c;
        }
        if (time > 2f) Destroy(gameObject);
    }

    public void Set(Transform? parent, Vector2 offset, Func<float, float> interp) {
        if(rect == null) rect = GetComponent<RectTransform>();
        initPos = rect.anchoredPosition;
        this.parent = parent;
        this.offset = offset;
        this.interp = interp;
        time = 0;
    }

    public void Set(Transform? parent, Vector2 offset) {
        Set(parent, offset, t => 0);
    }

    Vector2 Pos() {
        if(parent == null) return offset + initPos;

        Vector2 pos = (Vector2)cam.WorldToScreenPoint(parent.position) - canvas.sizeDelta / 2f;
        pos.x = Mathf.Clamp(pos.x, UI.screenBorder - canvas.sizeDelta.x / 2f, -UI.screenBorder + canvas.sizeDelta.x / 2f);
        pos.y = Mathf.Clamp(pos.y, UI.screenBorder - canvas.sizeDelta.y / 2f, -UI.screenBorder + canvas.sizeDelta.y / 2f);
        return offset + pos;
    }
}
