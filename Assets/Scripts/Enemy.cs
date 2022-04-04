using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GameManager.Instance().AddCharacter(this);
        _myHp = 100;
        _myDamage = 10;
        _myName = "Enemy";

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
        if (_player == null)
        {
            _player = GameObject.FindWithTag("Player").GetComponent<Player>(); // John-Lemon���� ����
        }
    }

    /// <summary>
    /// Attack:
    /// 1) _gameRound�� ���������� ������ 3�� ����
    /// 2) _gameRound�� 10�� �Ǹ� ������ Player�� ���̵��� ������ ����
    /// </summary>
    public override void Attack()
    {
        bool revisFinished = !_isFinished;
        if ((_myName == _whoseTurn) && revisFinished) // Character �κп��� if ((_myName == _whoseTurn)&&(_gameRound <= 10))�� �ۼ�
        {
            if (_gameRound == 10)
            {
                _player.GetHit(123456789);
            }
            else
            {
                AttackMotion();
                _player.GetHit(_myDamage); 
                _myDamage = _myDamage + 3;
            }
            
        }
    }

  
    public override void GetHit(float damage)
    {
        int _randomAttack= Random.Range(0, 10);
        base.GetHit(damage);
        if(_randomAttack >= 7)
        {
            _myHp = _myHp + 10;
            Debug.Log($"{_myName} Heal!");
        }
    }
}

