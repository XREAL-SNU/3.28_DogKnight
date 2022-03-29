using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private Player _player;
    private float _randomHeal;
    private int DAMAGE_INCREASE = 3;
    private int FINAL_ROUND = 10;
    private int PLAYER_HP = 100;

    public GameObject gameManager;
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
        gameManager.GetComponent<GameManager>().AddCharacter(this);
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
        Init();
        if(_player == null)
        {
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
        if (!_isFinished && _myName == _whoseTurn)
        {
            AttackMotion();
            if (_gameRound == FINAL_ROUND)
            {
                _myDamage = PLAYER_HP;
            } else
            {
                _myDamage = _myDamage + DAMAGE_INCREASE;
            }
            _player.GetHit(_myDamage);
        }

        
    }

    /// <summary>
    /// GetHit:
    /// 1) Player�� _randomAttack�� ������ ���
    /// 2) 30%�� Ȯ���� �ǰݽ� 10 ü�� ����
    ///   + Debug.Log($"{_myName} Heal!"); �߰�
    /// </summary>
    public override void GetHit(float damage)
    {

    }
}

