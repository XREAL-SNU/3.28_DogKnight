using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharSelectFrame : MonoBehaviour {
    public GameObject buttonPrefab;

    public void Set(List<Character> list, Action<Character> action) {
        UI.ClearChildren(gameObject);

        list.ForEach(c => {
            Button(c, action);
        });
    }

    private void Button(Character c, Action<Character> action) {
        GameObject b = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity);

        b.transform.SetParent(transform, false);
        b.GetComponent<Button>().onClick.AddListener(() => {
            action(c);
            Destroy(gameObject);
        });
        b.GetComponent<TextMeshProUGUI>().text = c.name;
    }
}
