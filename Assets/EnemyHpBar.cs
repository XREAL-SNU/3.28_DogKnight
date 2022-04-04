using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : HpInterface
{

    Enemy enemyscript;
    public Image bar;
    public Image blocker;


    // Start is called before the first frame update
    void Awake()
    {
        Enemy enemyscript = FindObjectOfType<Enemy>().GetComponent<Enemy>();
        enemyscript.AddObserver(this);

        Image bar = GetComponent<Image>();

    }

    public override void HpOnNotify(float hp_changed)
    {
        base.HpOnNotify(hp_changed);
    
        bar.fillAmount = hp_changed * 0.01f;

        blocker.enabled = false;




    }





}
