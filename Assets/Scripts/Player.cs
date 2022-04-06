using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
   
    private float _randomAttack;

    /// <summary>
    /// 1. Init: 초기화 기능
    /// 1) Subject에 Observer로 등록
    /// 2) _myName, _myHp, _myDamage 초기화
    /// 3) _myName은 무조건 "Player"로 할 것
    /// 4) _myHp, _myDamage는 100, 20으로 각각 초기화 (권장 사항)
    /// </summary>
    protected override void Init()
    {
        base.Init();
        _myName = "Player";
        _myHp = 100;
        _myHpMax = _myHp;
        _myDamage = 20;
        GameManager.Instance().AddCharacter(this.GetComponent<Player>());
    }

    private void Awake()
    {
        Init();
    }


    public override void Attack()
    {
        _randomAttack = Random.Range(0, 10);
        if (!_isFinished)
        {
            if (_myName == _whoseTurn)
            {
                if (_randomAttack <= 3)
                {
                    SpecialAttackMotion();
                    Debug.Log($"{_myName} Special Attack");
                    GameManager.Instance().GetCharacter("Enemy").GetHit(_myDamage+10);
                    
                }
                else
                {
                    AttackMotion();
                    GameManager.Instance().GetCharacter("Enemy").GetHit(_myDamage);
                    
                }
            }
        }
    }

    public override void GetHit(float damage)
    {
        base.GetHit(damage);

    }
}
