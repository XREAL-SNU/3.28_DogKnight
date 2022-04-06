using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private float _randomAttack;

    protected override void Init()
    {
        base.Init();
        _myName = "Player";
        _myHpMax = 100;
        _myHp = _myHpMax;
        _myDamage = 20;
        GameManager.Instance().AddCharacter("Player", this.GetComponent<Player>());
    }

    private void Awake()
    {
        Init();
    }

    public override void Attack()
    {
        if(_myName.Equals(_whoseTurn) && !_isFinished)
        {
            _randomAttack = Random.Range(0, 10);
            if (_randomAttack < 7)
            {
                AttackMotion();
                GameManager.Instance().GetCharacter("Enemy").GetHit(_myDamage);
            }
            else
            {
                SpecialAttackMotion();
                Debug.Log($"{_myName} Special Attack!");
                GameManager.Instance().GetCharacter("Enemy").GetHit(_myDamage + 10);
            }
        }
    }

    public override void GetHit(float damage)
    {
        base.GetHit(damage);
    }
}