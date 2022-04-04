using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnIndicator : MonoBehaviour {
    private Vector3 target;
    private bool playerTurn = true;
    public float speed;
    public Color player = Color.white, enemy = Color.red;

    public static Character next;

    SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameManager.Instance().TurnEndEvent += TurnEnd;
        transform.position = target = GameManager.Instance().NextCharacter().transform.position;
        next = GameManager.Instance().NextCharacter();
    }

    void Update() {
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 0.08f * 60);
        transform.rotation = Quaternion.Euler(90, 0, Time.time * speed);
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, playerTurn ? player : enemy, Time.deltaTime * 0.08f * 60);
    }

    void TurnEnd() {
        StartCoroutine(EndEnum());
        next = GameManager.Instance().NextCharacter();
    }

    IEnumerator EndEnum() {
        yield return new WaitForSeconds(1f);
        target = GameManager.Instance().NextCharacter().transform.position;
        playerTurn = GameManager.Instance().TeamMember(true).Contains(GameManager.Instance().NextCharacter());
    }
}
