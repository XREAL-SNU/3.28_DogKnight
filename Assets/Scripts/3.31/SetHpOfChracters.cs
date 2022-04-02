using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHpOfChracters 
{
    private static float FULL_HP = 100;

    private static Slider playerHpBar = GameObject.Find("PlayerHpBar").GetComponent<Slider>();
    private static Slider enemyHpBar = GameObject.Find("EnemyHpBar").GetComponent<Slider>();

    public static void updatePlayerHp(float playerHp)
    {
        playerHpBar.value = playerHp / FULL_HP;
    }

    public static void updateEnemyHp(float enemyHp)
    {
        enemyHpBar.value = enemyHp / FULL_HP;
    }
    
}
