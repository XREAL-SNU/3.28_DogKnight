using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
   
    private float _randomAttack;

    /// <summary>
    /// 1. Init: �ʱ�ȭ ���
    /// 1) Subject�� Observer�� ���
    /// 2) _myName, _myHp, _myDamage �ʱ�ȭ
    /// 3) _myName�� ������ "Player"�� �� ��
    /// 4) _myHp, _myDamage�� 100, 20���� ���� �ʱ�ȭ (���� ����)
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
