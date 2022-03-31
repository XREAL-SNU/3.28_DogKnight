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
        GameManager.Instance().AddCharacter(gameObject.GetComponent<Enemy>());
        _myName = "Enemy";
        _myHp = 100;
        _myDamage = 10;
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
        if(_player == null){
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        }
    }

    /// <summary>
    /// Attack:
    /// 1) _gameRound�� ���������� ������ 3�� ����
    /// 2) _gameRound�� 10�� �Ǹ� ������ Player�� ���̵��� ������ ����
    /// </summary>
    public override void Attack()
    {
        if(_isFinished || _myName != _whoseTurn) return;
        if(_gameRound<10){
            _player.GetHit(3*_gameRound + _myDamage);
        }
        else{
            _player.GetHit(999);
        }
        AttackMotion();
    }

    /// <summary>
    /// GetHit:
    /// 1) Player�� _randomAttack�� ������ ���
    /// 2) 30%�� Ȯ���� �ǰݽ� 10 ü�� ����
    ///   + Debug.Log($"{_myName} Heal!"); �߰�
    /// </summary>
    public override void GetHit(float damage)
    {
        _randomHeal = Random.Range(0, 10);
        if(((int)_randomHeal)%3 == 0){
            _myHp += 10;
            Debug.Log($"{_myName} Heal!");
        }
        else{
            base.GetHit(damage);
        }
    }
}

