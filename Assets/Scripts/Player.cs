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
                //������� �ִϸ��̼��� Ʈ���� Ŭ���� ���¸� �޾Ƽ� ���ø���̸� �����, ������̸� �������� �����Ϸ�������..��..��...
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