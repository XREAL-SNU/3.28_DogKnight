using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterFrame : MonoBehaviour {
    public Color teamColor = Color.white, enemyColor = Color.red, deadColor = new Color(0.7f, 0.7f, 0.7f), enemyDeadColor = Color.red, turnColor = Color.yellow, enemyTurnColor = Color.magenta;
    private const float HPBAR_WIDTH = 350f;

    private float deltaHp = 0f;
    private bool isEnemy, isDead;

    //objects
    public RectTransform hpBar, hpBarSub;
    public TextMeshProUGUI nameLabel;

    public Character character;

    void Update() {
        if (character == null) return;

        if (Mathf.Abs(deltaHp - character.hp) < 0.01f) deltaHp = character.hp;
        else deltaHp = Mathf.Lerp(deltaHp, character.hp, (character.hp >= deltaHp ? 0.08f * 2f : 0.08f) * 60f * Time.deltaTime);
        if (deltaHp <= character.hp) {
            //healing
            setHPSub(character.hp);
            setHP(deltaHp);
        }
        else {
            //taken damage
            setHPSub(deltaHp);
            setHP(character.hp);
        }

        if (character.dead) {
            if (!isDead) {
                isDead = true;
                nameLabel.color = isEnemy ? enemyDeadColor : deadColor;
            }
        }
        else{
            isDead = false;
            nameLabel.color = TurnIndicator.next == character ? (isEnemy ? enemyTurnColor : turnColor) : (isEnemy ? enemyColor : teamColor);
        }
    }

    public void Set(Character character, bool enemy) {
        this.character = character;
        nameLabel.text = character.name;
        nameLabel.color = enemy ? enemyColor : teamColor;
        deltaHp = character.maxHp;
        isEnemy = enemy;
        if(isEnemy) nameLabel.alignment = TextAlignmentOptions.Right;
    }

    private void setHP(float hp) {
        hpBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, HPBAR_WIDTH * Mathf.Clamp01(hp / character.maxHp));
    }

    private void setHPSub(float hp) {
        hpBarSub.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, HPBAR_WIDTH * Mathf.Clamp01(hp / character.maxHp));
    }
}
