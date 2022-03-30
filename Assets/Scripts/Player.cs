using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Enemy _enemy;
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
        GameManager.Instance.AddCharacter(this);
        _myHp = 100;
        _myDamage = 20;
        _myName = "Player";

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
        _enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>(); // John-Lemon���� ����
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
        bool revisFinished = !_isFinished;
        if (_myName == _whoseTurn && revisFinished)
        {
            int _randomAttack = Random.Range(0, 10);
            if (_randomAttack >= 7)
            {
                SpecialAttackMotion();
                float _damage = _myDamage + 10;
                Debug.Log($"{_myName} Special Attack!");
                _enemy.GetHit(_damage);
            }
            else
            {
                AttackMotion();
                _enemy.GetHit(_myDamage);
            }
        }
    }

    public override void GetHit(float damage)
    {
        base.GetHit(damage);
    }
}
