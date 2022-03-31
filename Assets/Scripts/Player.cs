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
        GameManager.Instance().AddCharacter(this);
        _myName = "Player";
        _myHp = 100; 
        _myDamage = 20;
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
        if(!_isFinished)
        {
            if (_myName == _whoseTurn)
            {
                int _randomAttack = Random.Range(0,10);
                if(_randomAttack < 3)
                {
                    SpecialAttackMotion();
                    Debug.Log($"{_myName} Special Attack!");
                    _enemy.GetHit(_myDamage*10);
                }
                else
                {
                    AttackMotion();
                    _enemy.GetHit(_myDamage);
                }
            }
        }
    }

    public override void GetHit(float damage)
    {
        _myHp -= damage; 
        if(_myHp <= 0)
        {
            DeadMotion();
            GameManager.Instance().EndNotify();
        }
        else
        {
            GetHitMotion();
            Debug.Log($"{_myName} HP: {_myHp}");
        }
    }
}
