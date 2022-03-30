using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private GameObject _enemy;
    private float _randomAttack;
    private Observer observer;
    private Subject subject;

    /// <summary>
    /// 1. Init: �ʱ�ȭ ���
    /// 1) Subject�� Observer�� ���
    /// 2) _myName, _myHp, _myDamage �ʱ�ȭ
    /// 3) _myName�� ������ "Player"�� �� ��
    /// 4) _myHp, _myDamage�� 100, 20���� ���� �ʱ�ȭ (���� ����)
    /// </summary>
    /// 

    protected override void Init()
    {
        base.Init();
        // subject�� observer�� ���
        this._myName = "Player";
        this._myHp = 100;
        this._myDamage = 20;
    }

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 1) _enemy�� �Ҵ��� �ȵƴٸ�,
    /// 2) GameObject.FindWithTag �̿��ؼ� _enemy �Ҵ�
    /// </summary>
    private void Start()
    {
        if (_enemy == null) 
        {
           _enemy = GameObject.FindWithTag("Enemy");
        } 
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
        int _randomAttack = Random.Range(0, 10);
        if (_randomAttack < 3)
        {
            SpecialAttackMotion();
            Debug.Log($"{_myName} Special Attack!");
        }
        else
        {
            AttackMotion();
            GetHit(_myDamage);
        }
    }

    public override void GetHit(float damage)
    {
        this._myHp -= damage;
        if (_myHp <= 0)
        {
            DeadMotion();
            subject.EndNotify();
        }
        else
        {
            GetHitMotion();
            Debug.Log($"{_myName} HP: {_myHp}");
        }
    }
}
