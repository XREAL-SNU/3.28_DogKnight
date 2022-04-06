using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character //�ڽ�Ŭ���� 
{
    private Enemy _enemy; //Enemy������ ����_enemy�ϱ�.
    private float _randomAttack;

    /// <summary>
    /// 1. Init: �ʱ�ȭ ���
    /// 1) Subject�� Observer�� ���
    /// 2) _myName, _myHp, _myDamage �ʱ�ȭ
    /// 3) _myName�� ������ "Player"�� �� ��
    /// 4) _myHp, _myDamage�� 100, 20���� ���� �ʱ�ȭ (���� ����)
    /// </summary>
    protected override void Init() //�ڽ�Ŭ�������� �������̵��ؼ� �ʱ�ȭ�ϴ� �͵�.
    {
        gamemanager.GetComponent<GameManager>().AddCharacter(this);
        base.Init();
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
        if (_enemy==null)/// 1) _enemy�� �Ҵ��� �ȵƴٸ�,
        {
            _enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>(); /// 2) GameObject.FindWithTag �̿��ؼ� _enemy �Ҵ�

        }
    }

    /// <summary>
    /// Attack:
    /// 1) Player�� 30%�� Ȯ���� ���ݷ��� �� ���� ������ ���� ��
    /// 
    /// 2) _randomAttack = Random.Range(0,10); ���� ���� ���� ����
    /// 
    ///   -> 0~9 ������ ���� �� �ϳ��� �������� �Ҵ����.
    ///   
    /// 3) _randomAttack �̿��ؼ� 30% Ȯ���� ���� ���ݷº��� 10 ���� ���� ����
    /// 
    /// 4) �̶��� AttackMotion() ���� SpecialAttackMotion() ȣ���� ��
    /// 
    ///    + Debug.Log($"{_myName} Special Attack!"); �߰�
    ///    
    /// 5) 70% Ȯ���� �ϴ� �Ϲ� ������ Character�� ���ִ� �ּ��� ����
    /// </summary>
    /// 
    public override void Attack()
    {
        int _randomAttack;
        float damage;
        if (!_isFinished && _myName == _whoseTurn)
        {
            _randomAttack = Random.Range(0, 10);
            if (0 <= _randomAttack && _randomAttack < 3)//�θ�Ŭ������ �ٸ� ����� �����ϰ���.
            {
                SpecialAttackMotion();
                damage = _myDamage + 10;//���� ���ݷº��� 10���ƾ�!
                Debug.Log($"{_myName} Special Attack!");
            }
            else if(_randomAttack>3)//70% Ȯ���� �ϴ� �Ϲ� ������ Character�� ���ִ� �ּ��� ����
            {
                damage = _myDamage;
            }

            _enemy.GetHit(damage);
        }
    }

    public override void GetHit(float damage)
    {
        base.GetHit(damage);
    }
}
