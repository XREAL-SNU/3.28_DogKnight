using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : AttackButton
{
    public GameObject buttonPrefab;
    public GameObject skillPane;

    public override void Clicked() {
        skillPane.SetActive(true);
        UI.ClearChildren(skillPane);
        if(GameManager.Instance().NextCharacter() is Player p) {
            foreach (Skill skill in p.skills) {
                Button(p, skill);
            }
        }
        BackButton();
    }

    private void Button(Player p, Skill skill) {
        GameObject b = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity);

        b.transform.SetParent(skillPane.transform, false);
        Button bb = b.GetComponent<Button>();
        bb.onClick.AddListener(() => {
            if(skill.target == Skill.TARGET.none) {
                skillPane.SetActive(false);
                GameManager.Instance().RoundNotify(c => {
                    skill.At(c, null);
                });
            }
            else {
                UI.SelectTarget(b.transform.position, GameManager.Instance().TeamMember(skill.target == Skill.TARGET.team),
                    t => {
                        skillPane.SetActive(false);
                        GameManager.Instance().RoundNotify(c => skill.At(c, t));
                        });
            }
            
        });
        bb.interactable = Player.mp >= skill.cost;
        b.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = skill.name;
        b.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Player.mp >= skill.cost ? Color.white : Color.gray;
        b.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = skill.cost + " MP";
    }

    private void BackButton() {
        GameObject b = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity);
        b.transform.SetParent(skillPane.transform, false);
        b.GetComponent<Button>().onClick.AddListener(() => {
            skillPane.SetActive(false);
            transform.parent.GetComponent<AttackFrame>().SetInteractable(true);
        });

        b.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "< Back";
        b.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.yellow;
        b.transform.GetChild(1).gameObject.SetActive(false);
    }
}
