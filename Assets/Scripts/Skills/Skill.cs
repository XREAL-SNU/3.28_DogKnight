using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skills/Skill", order = 3)]
public class Skill : ScriptableObject
{
    public string description;
    public int cost = 10;
    public Attack attack;

    public enum TARGET { //decides the target select UI
        none,
        enemy,
        team
    }

    public TARGET target = TARGET.enemy;

    public void At(Character c,Character target) {
        Player.mp -= cost;
        if(Player.mp < 0) Player.mp = 0;
        attack.At(c,target);
    }
}
