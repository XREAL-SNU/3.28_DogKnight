using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // 1. _enemy 변수 삭제 -> GetCharacter로 접근할 거임
    //private Enemy _enemy;
    private float _randomAttack;
    public Stack<Item> _itemStack;
    protected override void Init()
    {
        base.Init();
        _myName = "Player";
        _myHpMax = 100;
        _myHp = _myHpMax;
        _myDamage = 20;
        GameManager.Instance().AddCharacter(this.GetComponent<Player>());
    }

    private void Awake()
    {
        Init();
    }

    public override void Attack()
    {
        if (_myName.Equals(_whoseTurn) && !_isFinished)
        {
            _randomAttack = Random.Range(0, 10);
            if (_randomAttack < 7)
            {
                AttackMotion();
                // 1. GetCharacter로 Enemy 접근
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