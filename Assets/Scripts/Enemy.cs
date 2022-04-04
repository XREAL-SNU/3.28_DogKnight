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

      _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    /// <summary>
    /// Attack:
    /// 1) _gameRound�� ���������� ������ 3�� ����
    /// 2) _gameRound�� 10�� �Ǹ� ������ Player�� ���̵��� ������ ����
    /// </summary>
    public override void Attack()
    {




        if (_isFinished == false && _myName == _whoseTurn)	
        {

        if (_gameRound == 10 ) 
        {
            _myDamage = _player._myHp;
        }
        else if (_gameRound !=1)
        {
            _myDamage += 3;

        }

         _player.GetHit(_myDamage);	
            AttackMotion();

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

        _myHp -= damage;
        
        if (_myHp <=0)
        {
            DeadMotion();
            GameManager.Instance().EndNotify();
        }

        else
        {
            GetHitMotion();
            _randomHeal = Random.Range(0,10);

            if (_randomHeal <3)
            {
                _myHp += 10;
                Debug.Log($"{_myName} Heal!");
            }
                Debug.Log($"{_myName} HP: {_myHp}");

            }


    }


}

