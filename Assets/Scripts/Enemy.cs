using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    private Player _player;
    private float _randomHeal;

    /// <summary>
    /// 1. Init: �ʱ�ȭ ���
    /// 1) Subject�� Observer�� ���
    /// 2) _myName, _myHp, _myDamage �ʱ�ȭ
    /// 3) _myName�� ������ "Enemy"�� �� ��
    /// 4) _myHp, _myDamage�� 100, 10���� ���� �ʱ�ȭ (���� ����)
    /// </summary>
    protected override void Init()
    {
        base.Init();
        _myName = "Enemy";
        _myHp = 100;
        _myDamage = 10;
        GameManager.Instance().AddCharacter(this.GetComponent<Enemy>());
    }

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 1) _player�� �Ҵ��� �ȵƴٸ�,
    /// 2) GameObject.FindWithTag �̿��ؼ� _player �Ҵ�
    /// </summary>
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        other = _player;
    }

    /// <summary>
    /// Attack:
    /// 1) _gameRound�� ���������� ������ 3�� ����
    /// 2) _gameRound�� 10�� �Ǹ� ������ Player�� ���̵��� ������ ����
    /// </summary>
    public override void Attack()
    {
        if (!_isFinished && (_myName == _whoseTurn))
        {
            _myDamage += 3;
            if (_gameRound >= 10)
            {
                _myDamage = _player._myHp;
            }
            AttackMotion();
            other.GetHit(_myDamage);
            UIManager.Instance().SliderBarUpdate(other);

        }
    }

    /// <summary>
    /// GetHit:
    /// 1) Player�� _randomAttack�� ������ ���
    /// 2) 30%�� Ȯ���� �ǰݽ� 10 ü�� ����
    ///   + Debug.Log($"{_myName} Heal!"); �߰�
    /// </summary>
    public override void GetHit( float damage)
    {
        base.GetHit(damage);
        if(_myHp > 0)
        {
            if (isSkilled)
            {
                _myHp += 10;
                Debug.Log($"{_myName} Heal!");
                UIManager.Instance().SliderBarUpdate(this);
            }
        }   
    }
}

