using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour {
    public static UI main;
    public const float screenBorder = 5f;

    public Transform playerFrame, enemyFrame;
    public Color damageColor = Color.red, healColor = Color.green;

    void Awake() {
        Debug.Log(main);
        if (main != null && main != this)
            Destroy(gameObject);
        else
            main = this;

        //DontDestroyOnLoad(this);
    }

    public static void ClearChildren(GameObject o) {
        int n = o.transform.childCount;
        if (n <= 0) return;
        for (int i = n - 1; i >= 0; i--) {
            Destroy(o.transform.GetChild(i).gameObject);
        }
    }

    public static Canvas Canvas() {
        return FindObjectOfType<Canvas>();
    }

    public static void Damage(Transform? parent, Vector3 worldPos, int amount, Color color, System.Func<float, float> interp) {
        RectTransform screenRect = FindObjectOfType<Canvas>().gameObject.GetComponent<RectTransform>();
        Vector2 pos = (Vector2)Camera.main.WorldToScreenPoint(worldPos) - screenRect.sizeDelta / 2f;
        pos.x = Mathf.Clamp(pos.x, screenBorder - screenRect.sizeDelta.x / 2f, -screenBorder + screenRect.sizeDelta.x / 2f);
        pos.y = Mathf.Clamp(pos.y, screenBorder - screenRect.sizeDelta.y / 2f, -screenBorder + screenRect.sizeDelta.y / 2f);

        GameObject o = Instantiate(Resources.Load<GameObject>("UI/DamageIndicator"), Vector3.zero, Quaternion.identity);
        o.transform.SetParent(screenRect.transform, false);
        o.transform.SetAsFirstSibling();
        o.GetComponent<RectTransform>().anchoredPosition = pos;
        TextMeshProUGUI text = o.GetComponent<TextMeshProUGUI>();
        //text.enabled = true;
        text.color = color;
        text.text = amount.ToString();

        o.GetComponent<DamageIndicator>().Set(parent, new Vector2(UnityEngine.Random.Range(-30f, 30f), UnityEngine.Random.Range(-30f, 30f) + 100f), interp);
    }

    public static void Damage(Vector3 pos, int amount) {
        if (amount >= 0) Damage(null, pos, amount, main.damageColor, DamageIndicator.bounce);
        else Damage(null, pos, -amount, main.healColor, DamageIndicator.fin);
    }

    public static void Damage(Transform parent, int amount) {
        if (amount >= 0) Damage(parent, parent.position, amount, main.damageColor, DamageIndicator.bounce);
        else Damage(parent, parent.position, -amount, main.healColor, DamageIndicator.fin);
    }

    public static void AddCharacterUI(Character character, bool enemy) {
        GameObject o = Instantiate(Resources.Load<GameObject>("UI/CharBorder"), Vector3.zero, Quaternion.identity);
        o.GetComponent<CharacterFrame>().Set(character, enemy);
        o.transform.SetParent(enemy ? main.enemyFrame : main.playerFrame, false);
    }

    public static void SelectTarget(Vector2 pos, List<Character> list, Action<Character> action) {
        GameObject o = Instantiate(Resources.Load<GameObject>("UI/CharSelectFrame"), Vector3.zero, Quaternion.identity);
        o.GetComponent<CharSelectFrame>().Set(list, action);
        o.transform.SetParent(Canvas().transform, false);
        o.transform.position = pos;
    }
}
