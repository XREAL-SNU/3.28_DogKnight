using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHpOfChracters 
{
    private static int FULL_HP = 100;

    private static Slider playerHpBar = GameObject.Find("EnemyHpBar").GetComponent<Slider>();
    private static Slider enemyHpBar = GameObject.Find("EnemyHpBar").GetComponent<Slider>();

    static void updatePlayerHp(int playerHp)
    {
        playerHpBar.value = playerHp / FULL_HP;
    }

    static void updateEnemyHp(int enemyHp)
    {
        enemyHpBar.value = enemyHp / FULL_HP;
    }
    
}
