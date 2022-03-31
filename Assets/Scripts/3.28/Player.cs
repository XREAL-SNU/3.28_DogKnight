using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Enemy _enemy;
    private float _randomAttack;
    private int SPECIAL_ATTACK_MAX_VALUE = 3;
    private int SPECIAL_ATTACK_ADDITIOANL_VALUE = 10;

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
        _myDamage = 20;
    }

    /// <summary>
    /// 1) _enemy�� �Ҵ��� �ȵƴٸ�,
    /// 2) GameObject.FindWithTag �̿��ؼ� _enemy �Ҵ�
    /// </summary>
    private void Awake()
    {
        Init();
        if (_enemy == null)
        {
            _enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
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
        float _damage = 0;

        Debug.Log("Attack");
        if (!_isFinished && _myName == _whoseTurn)
        {
            int _randomAttack = Random.Range(0, 10);
            if(0 <= _randomAttack && _randomAttack < SPECIAL_ATTACK_MAX_VALUE)
            {
                SpecialAttackMotion();
                _damage = _myDamage + SPECIAL_ATTACK_ADDITIOANL_VALUE;
                Debug.Log($"{_myName} Special Attack!");
            } else
            {
                AttackMotion();
                _damage = _myDamage;
            }

            _enemy.GetHit(_damage);
        }
    }

    public override void GetHit(float damage)
    {
        base.GetHit(damage);

    }
}
