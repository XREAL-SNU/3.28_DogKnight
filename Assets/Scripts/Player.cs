using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Enemy _enemy;
    private float _randomAttack;
    
    
    protected override void Init()
    {
        base.Init();

        GameManager.Instance().AddCharacter(this);
        _myName = "Player";
        _myHp = 100;
        _myDamage = 20;
    }

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        if (_enemy == null)
        {
            _enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        }
    }

    public override void Attack()
    {
        if (!_isFinished)
        {
            if (_myName == _whoseTurn)
            {
                _randomAttack = Random.Range(0, 10);
                if (_randomAttack % 3 == 0)
                {
                    _myDamage += 10;
                    SpecialAttackMotion();

                    Debug.Log($"{_myName} Special Attack!");

                    _enemy.GetHit(_myDamage);
                    _myDamage -= 10;
                }
                else
                {
                    AttackMotion();
                    _enemy.GetHit(_myDamage);
                }
            }

            if (isDoubleAttack)
            {
                //원래라면 애니메이션의 트리거 클립의 상태를 받아서 어택모션이면 스페셜, 스페셜이면 어택으로 진행하려햇지만..귀..찮...
                 AttackMotion();
                SpecialAttackMotion();
                _enemy.GetHit(_myDamage);
                isDoubleAttack = false;
            }
        }
    }

    public override void GetHit(float damage)
    {
        base.GetHit(damage);
    }
}