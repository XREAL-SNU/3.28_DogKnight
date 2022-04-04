using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    //private Enemy _enemy;
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
        GameManager.Instance().AddCharacter(gameObject.GetComponent<Player>());
        _myName = "Player";
        _myHpMax = 100;
        _myHp = _myHpMax;
        _myDamage = 20;
    }

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// Attack:
    /// 1) Player�� 30%�� Ȯ���� ���ݷ��� �� ���� ������ ���� ��
    /// 2) _randomAttack = Random.Range(0,10); ���� ���� ���� ����
    ///   -> 0~9 ������ ���� �� �ϳ��� �������� �Ҵ����.
    /// 3) _randomAttack �̿��ؼ� 30% Ȯ���� ���� ���ݷº��� 10 ���� ���� ����
    /// 4) �̶��� AttackMotion() ���� SpecialAttackMotion() ȣ���� ��
    ///    + Debug.Log($"{_myName} Special Attack!"); �߰�
    /// 5) 70% Ȯ���� �ϴ� �Ϲ� ������ Character�� ���ִ� �ּ��� ����
    /// </summary>
    public override void Attack()
    {
        if(_isFinished || _myName != _whoseTurn) return;
        _randomAttack = Random.Range(0, 10);
        if(((int)_randomAttack)%3 == 0){
            SpecialAttackMotion();
            GameManager.Instance()GetCharacter("Enemy").GetHit(_myDamage + 10);
        }
        else{
            AttackMotion();
            GameManager.Instance()GetCharacter("Enemy").GetHit(_myDamage);
        }
    }

    public override void GetHit(float damage)
    {
        base.GetHit(damage);
    }
}
